using Aurora.Library.Common;
using Aurora.Library.Accounts;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Framework.Data.Entities
{
    /// <summary>
    /// User account entity.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// The ID of the account.
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// The name of the account.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The account type that indicates whether this account is for human or for program access.
        /// </summary>
        public AccountType Type { get; }
        /// <summary>
        /// The interface contains all bussiness logic to access the projects this account has permission to access.
        /// </summary>
        public AccountProjectsCollection Projects { get; set; }
        /// <summary>
        /// A collection of project entities for Entity Framework Core to track relationships.
        /// </summary>
        public ICollection<Project> ProjectCollection { get; }

        private readonly DatabaseContext _context;

        /// <summary>
        /// Instantiate an account.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="name">The account name.</param>
        /// <param name="type">The account type.</param>
        public Account(DatabaseContext context, string name, AccountType type)
        {
            _context = context;
            Name = name;
            Type = type;
            Projects = new(this, _context);
            ProjectCollection = new List<Project>();
        }
    }

    /// <summary>
    /// The interface contains all bussiness logic to access the projects this account has permission to access.
    /// </summary>
    public class AccountProjectsCollection
    {
        private readonly DatabaseContext _context;
        private readonly Account _account;
        private readonly IQueryable<Project> _projects;

        /// <summary>
        /// Instantiate an AccountProjectsCollection.
        /// </summary>
        /// <param name="account">The account to query.</param>
        /// <param name="context">The database context.</param>
        public AccountProjectsCollection(Account account, DatabaseContext context)
        {
            _account = account;
            _context = context;
            _projects = _context.Projects.Where(p => p.MemberCollection.Contains(_account) || p.Owner.Id == _account.Id);
        }

        /// <summary>
        /// Get a list of projects the account has permission to access.
        /// </summary>
        /// <param name="start">Where to start to query.</param>
        /// <param name="limit">The quantity of projects to query.</param>
        /// <returns></returns>
        public async Task<List<Project>> GetAsync(long start, int limit)
        {
            return await _projects.LongSkip(start - 1).Take(limit).ToListAsync();
        }

        /// <summary>
        /// Get a specific project by ID.
        /// </summary>
        /// <param name="projectId">The project ID.</param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">The account does not have the project.</exception>
        public async Task<Project> GetAsync(long projectId)
        {
            Project? targetProject = await _projects.FirstAsync(p => p.Id == projectId);
            if (targetProject == null)
            {
                throw new InvalidDataException("The project does not exist in the account.");
            }
            return targetProject;
        }

        /// <summary>
        /// Count how many projects in the account.
        /// </summary>
        /// <returns></returns>
        public async Task<long> CountAsync()
        {
            return await _projects.LongCountAsync();
        }

        /// <summary>
        /// Remove a project from the account.
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException"></exception>
        public async Task RemoveAsync(long projectId)
        {
            Project? targetProject = await _projects.FirstAsync(p => p.Id == projectId);
            if (targetProject == null)
            {
                throw new InvalidDataException("This account does not have this project.");
            }
            _account.ProjectCollection.Remove(targetProject);
        }
    }
}

using Aurora.Library.Common;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Framework.Data.Entities
{
    /// <summary>
    /// Project entity.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// The ID of the entity.
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// The name of the project.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The description for the project.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The account that owns the project.
        /// </summary>
        public Account Owner { get; set; }
        /// <summary>
        /// The accounts that are members of this project.
        /// </summary>
        public ProjectMembersCollection Memebers { get; set; }
        /// <summary>
        /// Collection for Entity Framework Core to track related entities.
        /// </summary>
        public ICollection<Account> MemberCollection { get; set; }
        /// <summary>
        /// The creation date.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Instantiate a project entity.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="owner"></param>
        public Project(DatabaseContext context, string name, string description, Account owner)
        {
            Name = name;
            Description = description;
            Owner = owner;
            MemberCollection = new List<Account>();
            Memebers = new(this, context);
            CreationDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Determines whether the project is empty or not.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return true;
        }
    }

    /// <summary>
    /// The interface that contains all bussiness logic to manipulate the members of a project.
    /// </summary>
    public class ProjectMembersCollection
    {
        private readonly DatabaseContext _context;
        private readonly Project _project;
        private readonly IQueryable<Account> _members;

        /// <summary>
        /// Instantiate a collection.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="context"></param>
        public ProjectMembersCollection(Project project, DatabaseContext context)
        {
            _project = project;
            _context = context;
            _members = _context.Accounts.Where(a => a.ProjectCollection.Contains(_project));
        }

        /// <summary>
        /// Get amount of members in the projects.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<List<Account>> GetAsync(long start, int limit)
        {
            return await _members.LongSkip(start - 1).Take(limit).ToListAsync();
        }

        /// <summary>
        /// Get a specific member by ID.
        /// </summary>
        /// <param name="memberId">The account ID of the member in the project.</param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException"></exception>
        public async Task<Account> GetAsync(long memberId)
        {
            Account? targetAccount = await _members.FirstAsync(m => m.Id == memberId);
            if (targetAccount == null)
            {
                throw new InvalidDataException("The account does not exist.");
            }
            return targetAccount;
        }

        /// <summary>
        /// Count how many members are in the project.
        /// </summary>
        /// <returns></returns>
        public async Task<long> CountAsync()
        {
            return await _members.LongCountAsync();
        }

        /// <summary>
        /// Add an existing account to the project as a member.
        /// </summary>
        /// <param name="memberId">The ID of an existing account.</param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task AddAsync(long memberId)
        {
            Account? newMember = await _context.Accounts.FindAsync(memberId);

            if (newMember == null)
            {
                throw new InvalidDataException("The account does not exists.");
            }

            if (await _members.AnyAsync(p => p.Id == newMember.Id))
            {
                throw new InvalidOperationException("The account is already a member of this project.");
            }

            _project.MemberCollection.Add(newMember);
        }

        /// <summary>
        /// Remove a member from the project.
        /// </summary>
        /// <param name="memberId">The account ID of a member in the project.</param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException"></exception>
        public async Task RemoveAsync(long memberId)
        {
            Account? targetMember = await _members.FirstAsync(a => a.Id == memberId);
            if (targetMember == null)
            {
                throw new InvalidDataException("The account does not exist in this project.");
            }
            _project.MemberCollection.Remove(targetMember);
        }
    }
}

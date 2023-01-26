using Framework.Core.Data.Services;
using Framework.Library.Accounts;
using Framework.Library.Common;
using Microsoft.EntityFrameworkCore;

namespace Framework.Core.Data.Models
{
    public class Account
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public AccountType Type { get; }
        public AccountProjectsCollection Projects { get; set; }
        public ICollection<Project> ProjectCollection { get; }

        private readonly DatabaseContext _context;

        public Account(DatabaseContext context, string name, AccountType type)
        {
            _context = context;
            Name = name;
            Type = type;
            Projects = new(this, _context);
            ProjectCollection = new List<Project>();
        }

        public IQueryable<Project> GetProjectCollectionQueryable()
        {
            return _context.Projects.Where(p => p.MemberCollection.Any(m => m.Id == Id));
        }
    }

    public class AccountProjectsCollection
    {
        private readonly DatabaseContext _context;
        private readonly Account _account;
        private readonly IQueryable<Project> _projects;

        public AccountProjectsCollection(Account account, DatabaseContext context)
        {
            _account = account;
            _context = context;
            _projects = _context.Projects.Where(p => p.MemberCollection.Contains(_account) || p.Owner.Id == _account.Id);
        }

        public async Task<List<Project>> GetAsync(long start, int limit)
        {
            return await _projects.LongSkip(start - 1).Take(limit).ToListAsync();
        }

        public async Task<Project> GetAsync(long projectId)
        {
            Project? targetProject = await _projects.FirstAsync(p => p.Id == projectId);
            if (targetProject == null)
            {
                throw new InvalidDataException("The project does not exist in the account.");
            }
            return targetProject;
        }

        public async Task<long> CountAsync()
        {
            return await _projects.LongCountAsync();
        }

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

using Framework.Core.Data.Services;
using Framework.Library.Common;
using Microsoft.EntityFrameworkCore;

namespace Framework.Core.Data.Models
{
    public class Project
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Account Owner { get; set; }
        public ProjectMembersCollection Memebers { get; set; }
        public ICollection<Account> MemberCollection { get; set; }
        public DateTime CreationDate { get; set; }

        public Project(DatabaseContext context, string name, string description, Account owner)
        {
            Name = name;
            Description = description;
            Owner = owner;
            MemberCollection = new List<Account>();
            Memebers = new(this, context);
            CreationDate = DateTime.UtcNow;
        }

        public bool IsEmpty()
        {
            return true;
        }
    }

    public class ProjectMembersCollection
    {
        private readonly DatabaseContext _context;
        private readonly Project _project;
        private readonly IQueryable<Account> _members;

        public ProjectMembersCollection(Project project, DatabaseContext context)
        {
            _project = project;
            _context = context;
            _members = _context.Accounts.Where(a => a.ProjectCollection.Contains(_project));
        }

        public async Task<List<Account>> GetAsync(long start, int limit)
        {
            return await _members.LongSkip(start - 1).Take(limit).ToListAsync();
        }

        public async Task<Account> GetAsync(long memberId)
        {
            Account? targetAccount = await _members.FirstAsync(m => m.Id == memberId);
            if (targetAccount == null)
            {
                throw new InvalidDataException("The account does not exist.");
            }
            return targetAccount;
        }

        public async Task<long> CountAsync()
        {
            return await _members.LongCountAsync();
        }

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

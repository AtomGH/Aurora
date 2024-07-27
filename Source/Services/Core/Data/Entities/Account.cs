using Aurora.Library.Common;
using Aurora.Library.Accounts;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Core.Data.Entities
{
    /// <summary>
    /// User account entity.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// The ID of the account.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name of the account.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The account type that indicates whether this account is for human or for program access.
        /// </summary>
        public AccountType Type { get; }
        public IQueryable<Project> Projects { get; set; }
        public List<int> ProjectIds { get; set; }

        /// <summary>
        /// Instantiate an account.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="id">The account ID.</param>
        /// <param name="name">The account name.</param>
        /// <param name="type">The account type.</param>
        public Account(DatabaseContext context, string name, AccountType type)
        {
            Name = name;
            Type = type;
            Projects = context.Projects.Where(p => p.MemberIds.Contains(Id));
            ProjectIds = new();
        }

        private Account()
        {
            // Required by EF Core
            Name = null!;
            Projects = null!;
            ProjectIds = null!;
        }
    }
}

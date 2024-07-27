using Aurora.Library.Common;
using Aurora.Library.Projects;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Core.Data.Entities
{
    /// <summary>
    /// Project entity.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// The ID of the entity.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name of the project.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The description for the project.
        /// </summary>
        public string Description { get; set; }
        public ProjectType Type { get; set; }
        public Project? ParentProject { get; private set; }
        public int OwnerId { get; set; }
        /// <summary>
        /// The account that owns the project.
        /// </summary>
        public Account Owner { get; set; }
        public IQueryable<Account> Members { get; set; }
        public List<int> MemberIds { get; set; }
        /// <summary>
        /// The creation date.
        /// </summary>
        public DateTime CreationDate { get; set; }

        private readonly DatabaseContext _databaseContext;

        /// <summary>
        /// Instantiate a project entity.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="owner"></param>
        public Project(DatabaseContext context, string name, string description, ProjectType type, Account owner)
        {
            _databaseContext = context;
            Name = name;
            Description = description;
            Type = type;
            Owner = owner;
            OwnerId = owner.Id;
            Members = context.Accounts.Where(a => a.ProjectIds.Contains(Id));
            MemberIds = new();
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

        public void SetParentProject(Project parent)
        {
            if (Type == ProjectType.Series)
            {
                throw new ArgumentException("Series project cannot have parent project.");
            }

            if (parent.Type != ProjectType.Series)
            {
                throw new ArgumentException("The parent project must be series type.");
            }

            ParentProject = parent;
        }
    }
}

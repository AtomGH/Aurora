using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Library.Projects
{
    /// <summary>
    /// The project information.
    /// </summary>
    public class ProjectInformation
    {
        /// <summary>
        /// The ID of the project.
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// The name of the project.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The description of the project.
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// The ID of the account who owns the project.
        /// </summary>
        public long OwnerId { get; set; }
    }
}

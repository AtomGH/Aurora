using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Library.Projects
{
    /// <summary>
    /// The parameters used to create a project.
    /// </summary>
    public class CreateProjectParameters
    {
        /// <summary>
        /// The name of the new project.
        /// </summary>
        public string ProjectName { get; set; } = string.Empty;
        /// <summary>
        /// The description of the new project.
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// The ID of the account who will own the new project.
        /// </summary>
        public long OwnerId { get; set; }
    }
}

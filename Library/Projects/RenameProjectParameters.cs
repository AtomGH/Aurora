using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Library.Projects
{
    /// <summary>
    /// The parameters used to rename a project.
    /// </summary>
    public class RenameProjectParameters
    {
        /// <summary>
        /// The new name for the project.
        /// </summary>
        public string NewName { get; set; } = string.Empty;
    }
}

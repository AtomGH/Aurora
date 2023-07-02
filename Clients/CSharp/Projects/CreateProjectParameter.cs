using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Library.Framework.Projects
{
    public class CreateProjectParameter
    {
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public long OwnerId { get; set; }
    }
}

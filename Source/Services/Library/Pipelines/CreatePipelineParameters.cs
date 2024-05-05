using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Library.Pipelines
{
    public class CreatePipelineParameters
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Dictionary<int, string> Steps { get; set; }
    }
}

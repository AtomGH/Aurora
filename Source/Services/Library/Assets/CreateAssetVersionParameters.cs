using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Library.Assets
{
    public class CreateAssetVersionParameters
    {
        public long Id { get; set; }
        public int Version { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Preview { get; set; } = string.Empty;
    }
}

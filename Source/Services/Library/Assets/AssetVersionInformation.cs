using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Library.Assets
{
    public class AssetVersionInformation
    {
        public long Id { get; set; }
        public int Version { get; set; }
        public string Description { get; set; }
        public string Preview { get; set; }
        public long AssetId { get; set; }
    }
}

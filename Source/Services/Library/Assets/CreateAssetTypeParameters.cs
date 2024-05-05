using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Library.Assets
{
    /// <summary>
    /// The parameters for creating an asset type.
    /// </summary>
    public class CreateAssetTypeParameters
    {
        /// <summary>
        /// The name of the new asset type.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The description of the new asset type.
        /// </summary>
        public string Description { get; set; } = string.Empty;
        public long PipelineId { get; set; }
    }
}

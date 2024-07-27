using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Library.Assets
{
    /// <summary>
    /// The parameters for creating an asset.
    /// </summary>
    public class CreateAssetParameters
    {
        /// <summary>
        /// The name of the asset.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The description of the asset.
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// The ID of the asset type for the asset, the asset type must be already exist.
        /// </summary>
        public int TypeId { get; set; }
        /// <summary>
        /// The ID of the project which the asset belongs to, the project must be already exist.
        /// </summary>
        public int ProjectId { get; set; }
        /// <summary>
        /// The ID of the operator who will be used to store the asset.
        /// </summary>
        public int OperatorId { get; set; }
        /// <summary>
        /// The token used to identify and access the asset from the operator.
        /// </summary>
        public Dictionary<string, string> Token { get; set; } = new();
        /// <summary>
        /// The tags of the asset.
        /// </summary>
        public Dictionary<string, string> Tags { get; set; } = new();
    }
}

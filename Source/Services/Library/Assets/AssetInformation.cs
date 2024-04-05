using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Library.Assets
{
    /// <summary>
    /// Used to contain the information of an asset and transferred between services.
    /// </summary>
    public class AssetInformation
    {
        /// <summary>
        /// The ID of the asset.
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// The name of the asset.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The description.
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// The ID of the asset type which the asset is.
        /// </summary>
        public long TypeId { get; set; }
        /// <summary>
        /// The ID of the project which the asset belongs to.
        /// </summary>
        public long ProjectId { get; set; }
        /// <summary>
        /// The ID of the operator who stores the asset.
        /// </summary>
        public long OperatorId { get; set; }
        /// <summary>
        /// The token used to identify and access the asset from the operator.
        /// </summary>
        public Dictionary<string, string> Token { get; set; } = new();
        /// <summary>
        /// The tags this asset has.
        /// </summary>
        public Dictionary<string, string> Tags { get; set; } = new();
    }
}

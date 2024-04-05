using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Library.Assets
{
    /// <summary>
    /// The information that describes an asset type.
    /// </summary>
    public class AssetTypeInformation
    {
        /// <summary>
        /// The type ID.
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// The type name.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The type description.
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}

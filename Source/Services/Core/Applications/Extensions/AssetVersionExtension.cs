using Aurora.Core.Data.Entities;
using Aurora.Library.Assets;

namespace Aurora.Core.Applications.Extensions
{
    public static class AssetVersionExtension
    {
        public static AssetVersionInformation ToInformation(this AssetVersion assetVersion)
        {
            return new()
            {
                Id = assetVersion.Id,
                Version = assetVersion.Version,
                Description = assetVersion.Description,
                Preview = assetVersion.Preview,
                AssetId = assetVersion.Asset.Id,
            };
        }
    }
}

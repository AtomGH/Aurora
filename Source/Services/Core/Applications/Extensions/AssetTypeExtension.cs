using Aurora.Core.Data.Entities;
using Aurora.Library.Assets;

namespace Aurora.Core.Applications.Extensions
{
    public static class AssetTypeExtension
    {
        public static AssetTypeInformation ToInformation(this AssetKind assetType)
        {
            return new()
            {
                Id = assetType.Id,
                Name = assetType.Name,
                Description = assetType.Description,
            };
        }
    }
}

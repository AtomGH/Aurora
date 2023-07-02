using Assets.Data.Entities;
using Aurora.Library.Assets;

namespace Aurora.Services.Assets.Extensions
{
    public static class AssetTypeExtensions
    {
        public static AssetTypeInformation ToInformation(this AssetType type)
        {
            return new AssetTypeInformation
            {
                Id = type.Id,
                Name = type.Name,
                Description = type.Description
            };
        }
    }
}

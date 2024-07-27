using Aurora.Core.Data.Entities;
using Aurora.Library.Assets;

namespace Aurora.Core.Applications.Extensions
{
    public static class AssetExtension
    {
        public static AssetInformation ToInformation(this Asset asset)
        {
            return new()
            {
                Id = asset.Id,
                Name = asset.Name,
                Description = asset.Description,
                TypeId = asset.Kind.Id,
                ProjectId = asset.Project.Id,
            };
        }
    }
}

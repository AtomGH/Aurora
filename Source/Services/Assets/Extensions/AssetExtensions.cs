using Assets.Data.Entities;
using Aurora.Library.Assets;

namespace Assets.Extensions
{
    public static class AssetExtensions
    {
        public static AssetInformation ToInformation(this Asset asset)
        {
            return new AssetInformation
            {
                Id = asset.Id,
                Name = asset.Name,
                Description = asset.Description,
                TypeId = asset.Type.Id,
                ProjectId = asset.ProjectId,
                OperatorId = asset.OperatorId,
                Token = asset.Token,
                Tags = new()
            };
        }
    }
}

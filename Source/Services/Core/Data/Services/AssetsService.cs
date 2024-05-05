using Aurora.Core.Data.Entities;
using Aurora.Library.Common;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Core.Data.Services
{
    public class AssetsService
    {
        private readonly DatabaseContext _context;
        private readonly IdentifierGenerator _idGenerator;

        public AssetsService(DatabaseContext context, IdentifierGenerator idGenerator)
        {
            _context = context;
            _idGenerator = idGenerator;
        }

        public async Task<Asset> GetAssetAsync(long projectId, long assetId)
        {
            Asset? targetAsset = await _context.Assets.Where(a => a.Id == assetId && a.Project.Id == projectId).FirstOrDefaultAsync();
            if (targetAsset == null)
            {
                throw new InvalidOperationException("The asset does not exist.");
            }
            return targetAsset;
        }

        public async Task<Asset> AddAssetAsync(string name, string description, AssetType assetType, Project project)
        {
            Asset newAsset = new(name, description, assetType, project);
            await _context.Assets.AddAsync(newAsset);
            return newAsset;
        }

        public async Task RemoveAssetAsync(long projectId, long assetId)
        {
            Asset? targetAsset = await _context.Assets.Where(a => a.Id == assetId && a.Project.Id == projectId).FirstOrDefaultAsync();
            if (targetAsset == null)
            {
                throw new InvalidOperationException("The asset does not exist.");
            }
            _context.Assets.Remove(targetAsset);
        }

        public async Task<AssetType> GetAssetTypeAsync(long projectId, long assetTypeId)
        {
            AssetType? targetType = await _context.AssetTypes.Where(t => t.Id == assetTypeId && t.Project.Id == projectId).FirstOrDefaultAsync();
            if (targetType == null)
            {
                throw new InvalidOperationException("The asset type does not exist.");
            }
            return targetType;
        }

        public async Task<AssetType> AddAssetTypeAsync(string name, string description, Pipeline pipeline, Project project)
        {
            AssetType newType = new(name, description, pipeline, project);
            await _context.AssetTypes.AddAsync(newType);
            return newType;
        }

        public async Task RemoveAssetTypeAsync(long projectId, long assetTypeId)
        {
            AssetType? targetType = await _context.AssetTypes.Where(t => t.Id == assetTypeId && t.Project.Id == projectId).FirstOrDefaultAsync();
            if (targetType == null)
            {
                throw new InvalidOperationException("The asset type does not exist.");
            }
            if (await _context.Assets.AnyAsync(a => a.Type.Id == assetTypeId))
            {
                throw new InvalidOperationException("The asset type is still in use.");
            }
            _context.AssetTypes.Remove(targetType);
        }

        public async Task<AssetVersion> GetAssetVersionAsync(long projectId, long assetId, long versionId)
        {
            AssetVersion? targetVersion = await _context.AssetVersions.Where(v => v.Id == versionId && v.Asset.Id == assetId && v.Asset.Project.Id == projectId).FirstOrDefaultAsync();
            if (targetVersion == null)
            {
                throw new InvalidOperationException("The asset version does not exist.");
            }
            return targetVersion;
        }

        public async Task<AssetVersion> AddAssetVersionAsync(string description, string preview, Asset asset)
        {
            AssetVersion newVersion = new(asset.IncreaseVersionCounter(), description, preview, asset);
            await _context.AssetVersions.AddAsync(newVersion);
            return newVersion;
        }
    }
}

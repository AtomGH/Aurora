using Aurora.Core.Data.Entities;
using Aurora.Library.Common;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Core.Data.Services
{
    public class AssetsService
    {
        private readonly DatabaseContext _context;

        public AssetsService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Asset> GetAssetAsync(int projectId, int assetId)
        {
            Asset? targetAsset = await _context.Assets.Where(a => a.Id == assetId && a.Project.Id == projectId).FirstOrDefaultAsync();
            if (targetAsset == null)
            {
                throw new InvalidOperationException("The asset does not exist.");
            }
            return targetAsset;
        }

        public async Task<Asset> AddAssetAsync(string name, string description, AssetKind assetKind, Project project)
        {
            Asset newAsset = new(_context, name, description, assetKind, project);
            await _context.Assets.AddAsync(newAsset);
            return newAsset;
        }

        public async Task RemoveAssetAsync(int projectId, int assetId)
        {
            Asset? targetAsset = await _context.Assets.Where(a => a.Id == assetId && a.Project.Id == projectId).FirstOrDefaultAsync();
            if (targetAsset == null)
            {
                throw new InvalidOperationException("The asset does not exist.");
            }
            _context.Assets.Remove(targetAsset);
        }

        public async Task<AssetKind> GetAssetKindAsync(int projectId, int assetTypeId)
        {
            AssetKind? targetKind = await _context.AssetKinds.Where(t => t.Id == assetTypeId && t.Project.Id == projectId).FirstOrDefaultAsync();
            if (targetKind == null)
            {
                throw new InvalidOperationException("The asset type does not exist.");
            }
            return targetKind;
        }

        public async Task<AssetKind> AddAssetKindAsync(string name, string description, Pipeline pipeline, Project project)
        {
            AssetKind newKind = new(name, description, pipeline, project);
            await _context.AssetKinds.AddAsync(newKind);
            return newKind;
        }

        public async Task RemoveAssetKindAsync(int projectId, int assetTypeId)
        {
            AssetKind? targetKind = await _context.AssetKinds.Where(t => t.Id == assetTypeId && t.Project.Id == projectId).FirstOrDefaultAsync();
            if (targetKind == null)
            {
                throw new InvalidOperationException("The asset type does not exist.");
            }
            if (await _context.Assets.AnyAsync(a => a.Kind.Id == assetTypeId))
            {
                throw new InvalidOperationException("The asset type is still in use.");
            }
            _context.AssetKinds.Remove(targetKind);
        }

        public async Task<AssetVersion> GetAssetVersionAsync(int projectId, int assetId, int versionId)
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

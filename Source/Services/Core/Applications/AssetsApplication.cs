using Aurora.Core.Applications.Extensions;
using Aurora.Core.Data;
using Aurora.Core.Data.Entities;
using Aurora.Library.Assets;
using Aurora.Library.Common;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Core.Applications
{
    /// <summary>
    /// Application layer that contains all bussiness logic to access assets.
    /// </summary>
    public class AssetsApplication
    {
        private readonly DataService _data;

        /// <summary>
        /// Instantiate an assets application.
        /// </summary>
        /// <param name="assets"></param>
        public AssetsApplication(DataService data)
        {
            _data = data;
        }

        /// <summary>
        /// Get a list of assets.
        /// </summary>
        /// <returns></returns>
        public async Task<RangeQueryResult<AssetInformation>> QueryAssetsAsync(QueryAssetParameters parameters)
        {
            List<Asset> queryResult = await _data.Database.Assets.Where(a => a.Project.Id == parameters.ProjectId).Skip(parameters.Start - 1).Take(parameters.Limit).ToListAsync();
            List<AssetInformation> listOfInformations = new();
            foreach (Asset asset in queryResult)
            {
                listOfInformations.Add(asset.ToInformation());
            }
            long totalQuantity = await _data.Database.Assets.Where(a => a.Project.Id == parameters.ProjectId).CountAsync();

            return new(totalQuantity, listOfInformations);
        }

        public async Task<AssetInformation> GetAssetAsync(int projectId, int assetId)
        {
            Asset targetAsset = await _data.Assets.GetAssetAsync(projectId, assetId);
            return targetAsset.ToInformation();
        }

        /// <summary>
        /// Create an asset.
        /// </summary>
        public async Task<AssetInformation> CreateAssetAsync(CreateAssetParameters parameters)
        {
            Project targetProject = await _data.Projects.GetAsync(parameters.ProjectId);
            AssetKind targetKind = await _data.Assets.GetAssetKindAsync(parameters.ProjectId, parameters.TypeId);
            Asset newAsset = await _data.Assets.AddAssetAsync(parameters.Name, parameters.Description, targetKind, targetProject);
            await _data.SaveAsync();
            return newAsset.ToInformation();
        }

        /// <summary>
        /// Delete an asset.
        /// </summary>
        public async Task DeleteAssetAsync(int projectId, int id)
        {
            await _data.Assets.RemoveAssetAsync(projectId, id);
            await _data.SaveAsync();
        }

        public async Task<RangeQueryResult<AssetTypeInformation>> QueryAssetTypesAsync(int projectId, RangeQueryParameter parameters)
        {
            List<AssetKind> listOfAssets = await _data.Database.AssetKinds.Where(a => a.Project.Id == projectId).Skip(parameters.Start - 1).Take(parameters.Limit).ToListAsync();
            List<AssetTypeInformation> listOfInformations = new();
            foreach (AssetKind assetType in listOfAssets)
            {
                listOfInformations.Add(assetType.ToInformation());
            }
            long totalQuantity = await _data.Database.AssetKinds.Where(a => a.Project.Id == projectId).CountAsync();
            return new(totalQuantity, listOfInformations);
        }

        public async Task<AssetTypeInformation> GetAssetTypeAsync(int projectId, int assetTypeId)
        {
            AssetKind targetKind = await _data.Assets.GetAssetKindAsync(projectId, assetTypeId);
            return targetKind.ToInformation();
        }

        public async Task<AssetTypeInformation> AddAssetTypeAsync(int projectId, CreateAssetTypeParameters parameters)
        {
            Project targetProject = await _data.Projects.GetAsync(projectId);
            Pipeline targetPipeline = await _data.Pipelines.GetPipelineAsync(projectId, parameters.PipelineId);
            AssetKind newAssetKind = await _data.Assets.AddAssetKindAsync(parameters.Name, parameters.Description, targetPipeline, targetProject);
            await _data.SaveAsync();
            return newAssetKind.ToInformation();
        }

        public async Task DeleteAssetTypeAsync(int projectId, int assetKindId)
        {
            await _data.Assets.RemoveAssetKindAsync(projectId, assetKindId);
            await _data.SaveAsync();
        }

        public async Task<RangeQueryResult<AssetVersionInformation>> QueryAssetVersionsAsync(int projectId, int assetId, RangeQueryParameter parameters)
        {
            List<AssetVersion> listOfVersions = await _data.Database.AssetVersions.Where(v => v.Asset.Id == assetId && v.Asset.ProjectId == projectId).Skip(parameters.Start - 1).Take(parameters.Limit).ToListAsync();
            List<AssetVersionInformation> listOfInformations = new();
            foreach (AssetVersion version in listOfVersions)
            {
                listOfInformations.Add(version.ToInformation());
            }
            long totalQuantity = await _data.Database.AssetVersions.Where(v => v.Asset.Id == assetId).CountAsync();
            return new(totalQuantity, listOfInformations);
        }

        public async Task<AssetVersionInformation> GetAssetVersionAsync(int projectId, int assetId, int versionId)
        {
            AssetVersion targetVersion = await _data.Assets.GetAssetVersionAsync(projectId, assetId, versionId);
            return targetVersion.ToInformation();
        }

        public async Task<AssetVersionInformation> AddAssetVersionAsync(int projectId, int assetId, CreateAssetVersionParameters parameters)
        {
            Asset targetAsset = await _data.Assets.GetAssetAsync(projectId, assetId);
            AssetVersion newVersion = await _data.Assets.AddAssetVersionAsync(parameters.Description, parameters.Preview, targetAsset);
            await _data.SaveAsync();
            return newVersion.ToInformation();
        }
    }
}

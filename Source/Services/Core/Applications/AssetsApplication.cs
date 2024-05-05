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
            List<Asset> queryResult = await _data.Database.Assets.Where(a => a.Project.Id == parameters.ProjectId).LongSkip(parameters.Start - 1).Take(parameters.Limit).ToListAsync();
            List<AssetInformation> listOfInformations = new();
            foreach (Asset asset in queryResult)
            {
                listOfInformations.Add(asset.ToInformation());
            }
            long totalQuantity = await _data.Database.Assets.Where(a => a.Project.Id == parameters.ProjectId).CountAsync();

            return new(totalQuantity, listOfInformations);
        }

        public async Task<AssetInformation> GetAssetAsync(long projectId, long assetId)
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
            AssetType targetType = await _data.Assets.GetAssetTypeAsync(parameters.ProjectId, parameters.TypeId);
            Asset newAsset = await _data.Assets.AddAssetAsync(parameters.Name, parameters.Description, targetType, targetProject);
            await _data.SaveAsync();
            return newAsset.ToInformation();
        }

        /// <summary>
        /// Delete an asset.
        /// </summary>
        public async Task DeleteAssetAsync(long id, long projectId)
        {
            await _data.Assets.RemoveAssetAsync(projectId, id);
            await _data.SaveAsync();
        }

        public async Task<RangeQueryResult<AssetTypeInformation>> QueryAssetTypesAsync(long projectId, RangeQueryParameter parameters)
        {
            List<AssetType> listOfAssets = await _data.Database.AssetTypes.Where(a => a.Project.Id == projectId).LongSkip(parameters.Start - 1).Take(parameters.Limit).ToListAsync();
            List<AssetTypeInformation> listOfInformations = new();
            foreach (AssetType assetType in listOfAssets)
            {
                listOfInformations.Add(assetType.ToInformation());
            }
            long totalQuantity = await _data.Database.AssetTypes.Where(a => a.Project.Id == projectId).CountAsync();
            return new(totalQuantity, listOfInformations);
        }

        public async Task<AssetTypeInformation> GetAssetTypeAsync(long projectId, long assetTypeId)
        {
            AssetType targetType = await _data.Assets.GetAssetTypeAsync(projectId, assetTypeId);
            return targetType.ToInformation();
        }

        public async Task<AssetTypeInformation> AddAssetTypeAsync(long projectId, CreateAssetTypeParameters parameters)
        {
            Project targetProject = await _data.Projects.GetAsync(projectId);
            Pipeline targetPipeline = await _data.Pipelines.GetPipelineAsync(projectId, parameters.PipelineId);
            AssetType newAssetType = await _data.Assets.AddAssetTypeAsync(parameters.Name, parameters.Description, targetPipeline, targetProject);
            await _data.SaveAsync();
            return newAssetType.ToInformation();
        }

        public async Task DeleteAssetTypeAsync(long projectId, long assetTypeId)
        {
            await _data.Assets.RemoveAssetTypeAsync(projectId, assetTypeId);
            await _data.SaveAsync();
        }

        public async Task<RangeQueryResult<AssetVersionInformation>> QueryAssetVersionsAsync(long projectId, long assetId, RangeQueryParameter parameters)
        {
            List<AssetVersion> listOfVersions = await _data.Database.AssetVersions.Where(v => v.Asset.Id == assetId).LongSkip(parameters.Start - 1).Take(parameters.Limit).ToListAsync();
            List<AssetVersionInformation> listOfInformations = new();
            foreach (AssetVersion version in listOfVersions)
            {
                listOfInformations.Add(version.ToInformation());
            }
            long totalQuantity = await _data.Database.AssetVersions.Where(v => v.Asset.Id == assetId).CountAsync();
            return new(totalQuantity, listOfInformations);
        }

        public async Task<AssetVersionInformation> GetAssetVersionAsync(long projectId, long assetId, long versionId)
        {
            AssetVersion targetVersion = await _data.Assets.GetAssetVersionAsync(projectId, assetId, versionId);
            return targetVersion.ToInformation();
        }

        public async Task<AssetVersionInformation> AddAssetVersionAsync(long projectId, long assetId, CreateAssetVersionParameters parameters)
        {
            Asset targetAsset = await _data.Assets.GetAssetAsync(projectId, assetId);
            AssetVersion newVersion = await _data.Assets.AddAssetVersionAsync(parameters.Description, parameters.Preview, targetAsset);
            await _data.SaveAsync();
            return newVersion.ToInformation();
        }
    }
}

// Controller: Responsible for handling HTTP requests and returning responses, also it is a compatibility adaption layer for different version of the same API.
// Application: The starting point of the bussiness logic, to manipulate bussiness data, this application layer is all you need, contains some high-level validation logic and data access logic, it is the first gate to prevent invalid data from entering the database.
// Data: Interact with the database, S3, Redis, used to manipulate data, contains fundamental validation logic and all data access logic, it is the last gate to prevent invalid data from entering the database.

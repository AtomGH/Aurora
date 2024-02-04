using Aurora.Library.Assets;

namespace Aurora.Core.Applications
{
    /// <summary>
    /// Application layer that contains all bussiness logic to access assets.
    /// </summary>
    public class AssetsApplication
    {
        private readonly AssetsOperator _assets;

        /// <summary>
        /// Instantiate an assets application.
        /// </summary>
        /// <param name="assets"></param>
        public AssetsApplication(AssetsOperator assets)
        {
            _assets = assets;
        }

        /// <summary>
        /// Get a list of assets.
        /// </summary>
        /// <returns></returns>
        public async Task<List<AssetInformation>> GetAssetAsync(QueryAssetParameters parameters)
        {
            return await _assets.QueryAsync(parameters);
        }

        /// <summary>
        /// Create an asset.
        /// </summary>
        public async Task<AssetInformation> CreateAssetAsync(CreateAssetParameters parameters)
        {
            return await _assets.CreateAsync(parameters);
        }

        /// <summary>
        /// Delete an asset.
        /// </summary>
        public async Task DeleteAssetAsync(long id, long projectId)
        {
            AssetInformation assetInfomation = await _assets.GetAsync(id);
            if (assetInfomation.ProjectId != projectId)
            {
                throw new Exception("Cannot find the asset with the provided ID in the project.");
            }
            await _assets.DeleteAsync(id);
        }
    }
}

// Controller: Responsible for handling HTTP requests and returning responses, also it is a compatibility adaption layer for different version of the same API.
// Application: The starting point of the bussiness logic, to manipulate bussiness data, this application layer is all you need, contains some high-level validation logic and data access logic, it is the first gate to prevent invalid data from entering the database.
// Data: Interact with the database, S3, Redis, used to manipulate data, contains fundamental validation logic and all data access logic, it is the last gate to prevent invalid data from entering the database.

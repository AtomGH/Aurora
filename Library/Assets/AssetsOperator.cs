using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Aurora.Library.Assets
{
    /// <summary>
    /// The API client for the Assets service.
    /// </summary>
    public class AssetsOperator
    {
        private readonly HttpClient _client;

        /// <summary>
        /// Initialize a new instance of assets API client.
        /// </summary>
        /// <param name="client">HttpClient used to send API requests to the assets services.</param>
        public AssetsOperator(HttpClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Get all assets from the assets service.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<AssetInformation>> QueryAsync(QueryAssetParameters parameters)
        {
            HttpResponseMessage response = await _client.GetAsync("/assets" + "?" + parameters.ToQueryString());
            response.EnsureSuccessStatusCode();
            List<AssetInformation>? assets = await response.Content.ReadFromJsonAsync<List<AssetInformation>>();
            return assets ?? throw new Exception("Cannot read valid content from the response.");
        }

        /// <summary>
        /// Get a specific asset by its ID.
        /// </summary>
        /// <param name="id">The asset ID, must be already exist.</param>
        /// <returns></returns>
        /// <exception cref="Exception">The response returned from the assets service is not recognizable.</exception>
        public async Task<AssetInformation> GetAsync(long id)
        {
            HttpResponseMessage response = await _client.GetAsync("/assets/" + id.ToString());
            response.EnsureSuccessStatusCode();
            AssetInformation? asset = await response.Content.ReadFromJsonAsync<AssetInformation>();
            return asset ?? throw new Exception("Cannot read valid content from the response.");
        }

        /// <summary>
        /// Create an asset with the given parameters.
        /// </summary>
        /// <param name="parameters">The parameters that must be filled to create an asset.</param>
        /// <returns>The information of the newly created asset.</returns>
        /// <exception cref="Exception">The response returned from the assets service is not recognizable.</exception>
        public async Task<AssetInformation> CreateAsync(CreateAssetParameters parameters)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("/assets", parameters);
            response.EnsureSuccessStatusCode();
            AssetInformation? asset = await response.Content.ReadFromJsonAsync<AssetInformation>();
            return asset ?? throw new Exception("Cannot read valid content from the response.");
        }

        /// <summary>
        /// Delete an asset by its ID.
        /// </summary>
        /// <param name="id">The asset ID.</param>
        /// <returns></returns>
        public async Task DeleteAsync(long id)
        {
            HttpResponseMessage response = await _client.DeleteAsync("/assets/" + id.ToString());
            response.EnsureSuccessStatusCode();
        }
    }
}

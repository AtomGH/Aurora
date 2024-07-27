using Aurora.Core.Applications;
using Aurora.Library.Assets;
using Aurora.Library.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aurora.Core.Controllers
{
    [Route("projects/{projectId}/assets")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly AssetsApplication _application;

        public AssetsController(AssetsApplication application)
        {
            _application = application;
        }

        [HttpGet]
        public async Task<IActionResult> QueryAsync([FromQuery] QueryAssetParameters parameters)
        {
            RangeQueryResult<AssetInformation> result = await _application.QueryAssetsAsync(parameters);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int projectId, int id)
        {
            AssetInformation targetAsset = await _application.GetAssetAsync(projectId, id);
            return Ok(targetAsset);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateAssetParameters parameters)
        {
            AssetInformation newAsset = await _application.CreateAssetAsync(parameters);
            return Created("/projects/" + parameters.ProjectId + "/assets/" + newAsset.Id, newAsset);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(int id, int projectId)
        {
            await _application.DeleteAssetAsync(projectId, id);
            return Ok();
        }

        [HttpGet("{id}/versions")]
        public async Task<IActionResult> QueryVersionsAsync(int id, int projectId, [FromQuery] RangeQueryParameter parameters)
        {
            RangeQueryResult<AssetVersionInformation> result = await _application.QueryAssetVersionsAsync(projectId, id, parameters);
            return Ok(result);
        }

        [HttpGet("{id}/versions/{versionId}")]
        public async Task<IActionResult> GetVersionAsync(int id, int projectId, int versionId)
        {
            AssetVersionInformation targetVersion = await _application.GetAssetVersionAsync(projectId, id, versionId);
            return Ok(targetVersion);
        }

        [HttpPost("{id}/versions")]
        public async Task<IActionResult> CreateVersionAsync(int id, int projectId, CreateAssetVersionParameters parameters)
        {
            AssetVersionInformation newVersion = await _application.AddAssetVersionAsync(projectId, id, parameters);
            return Created("/projects/" + projectId + "/assets/" + id + "/versions/" + newVersion.Id, newVersion);
        }
    }
}

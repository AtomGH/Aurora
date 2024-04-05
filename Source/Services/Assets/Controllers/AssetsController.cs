using Assets.Data;
using Assets.Data.Entities;
using Assets.Extensions;
using Aurora.Library.Assets;
using Aurora.Library.Common;
using Aurora.Services.Assets.Applications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assets.Controllers
{
    [Route("[controller]")]
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
            RangeQueryResult<AssetInformation> result = await _application.QueryAsync(parameters);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateAssetParameters parameters)
        {
            AssetInformation newAsset = await _application.CreateAssetAsync(parameters);
            return Created("/assets/" + newAsset.Id.ToString(), newAsset);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            AssetInformation targetAsset = await _application.GetAsync(id);
            return Ok(targetAsset);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(long id)
        {
            await _application.DeleteAssetAsync(id);
            return Ok();
        }
    }
}

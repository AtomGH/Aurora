using Aurora.Library.Assets;
using Aurora.Library.Common;
using Aurora.Services.Assets.Applications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assets.Controllers
{
    [Route("assets/[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly AssetTypesApplication _application;

        public TypesController(AssetTypesApplication application)
        {
            _application = application;
        }

        [HttpGet]
        public async Task<IActionResult> QueryAsync([FromQuery] RangeQueryParameter parameters)
        {
            RangeQueryResult<AssetTypeInformation> result = await _application.QueryAsync(parameters);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateAssetTypeParameters parameters)
        {
            AssetTypeInformation newAssetType = await _application.CreateAsync(parameters);
            return Created("/assets/types/" + newAssetType.Id.ToString(), newAssetType);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            await _application.DeleteAsync(id);
            return Ok();
        }
    }
}

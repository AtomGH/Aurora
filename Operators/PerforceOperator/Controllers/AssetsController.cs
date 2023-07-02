using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Perforce.P4;

namespace PerforceOperator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
        }
    }
}

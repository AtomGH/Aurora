using Aurora.Framework.Applications;
using Aurora.Library.Framework;
using Microsoft.AspNetCore.Mvc;

namespace Aurora.Framework.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InstancesController : ControllerBase
    {
        private readonly InstanceApplication _app;

        public InstancesController(InstanceApplication application)
        {
            _app = application;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync()
        {
            InstanceInformation newInstance = await _app.RegisterAsync();
            return Ok(newInstance);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> RefreshAsync(int id, [FromBody] InstanceRefreshParameter parameter)
        {
            await _app.RefreshAsync(id, parameter);
            return Ok();
        }
    }
}

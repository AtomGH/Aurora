using Aurora.Framework.Applications;
using Aurora.Framework.Data;
using Microsoft.AspNetCore.Mvc;

namespace Aurora.Framework.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ConfigurationsController : ControllerBase
    {
        private readonly InstanceApplication _instance;
        private readonly DatabaseContext _database;

        public ConfigurationsController(InstanceApplication instanceApplication, DatabaseContext databaseContext)
        {
            _instance = instanceApplication;
            _database = databaseContext;
        }

        [HttpPost("initialization")]
        public async Task<IActionResult> InitializeAsync()
        {
            await _database.Database.EnsureCreatedAsync();
            if (_instance.IsInitialized())
            {
                return Ok();
            }
            await _instance.InitializeAsync();
            return Ok();
        }
    }
}

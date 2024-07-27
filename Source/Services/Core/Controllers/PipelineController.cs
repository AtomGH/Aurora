using Aurora.Core.Applications;
using Aurora.Library.Assets;
using Aurora.Library.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aurora.Core.Controllers
{
    [Route("projects/{projectId}/assets/types/{typeId}/[controller]")]
    [ApiController]
    public class PipelineController : ControllerBase
    {
        private readonly PipelinesApplication _application;

        public PipelineController(PipelinesApplication application)
        {
            _application = application;
        }
    }
}

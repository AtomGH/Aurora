using Aurora.Core.Applications;
using Aurora.Library.Assets;
using Aurora.Library.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aurora.Core.Controllers
{
    [Route("projects/{projectId}/assets/types/{typeId}/[controller]")]
    [ApiController]
    public class PipelineStepsController : ControllerBase
    {
        private readonly AssetsApplication _application;

        public PipelineStepsController(AssetsApplication application)
        {
            _application = application;
        }
    }
}

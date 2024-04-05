using Aurora.Framework.Applications;
using Aurora.Library.Common;
using Microsoft.AspNetCore.Mvc;

namespace Aurora.Framework.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IdentifiersController : ControllerBase
    {
        private readonly IdentifierApplication _identifierGenerator;

        public IdentifiersController(IdentifierApplication identifierApplication)
        {
            _identifierGenerator = identifierApplication;
        }

        [HttpGet]
        public IActionResult Get([FromQuery]RangeQueryParameter parameter)
        {
            List<long> listOfGeneratedIDs = new();

            for (int i = 0; i < parameter.Limit; i++)
            {
                listOfGeneratedIDs.Add(_identifierGenerator.Get());
            }
            return Ok(new RangeQueryResult<long>(listOfGeneratedIDs.Count, listOfGeneratedIDs));
        }
    }
}

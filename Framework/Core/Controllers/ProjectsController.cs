using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        [HttpGet]
        public async IActionResult GetListOfProjectsAsync()
        {
            return Ok();
        }

        [HttpPost]
        public async IActionResult CreateProjectAsync()
        {
            return Created();
        }

        [HttpPut("{id}")]
        public async IActionResult ModifyProjectAsync(int id)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async IActionResult DeleteProjectAsync(int id)
        {
            return Ok();
        }
    }
}

using Framework.Core.Applications;
using Framework.Library.Common;
using Framework.Library.Projects;
using Microsoft.AspNetCore.Mvc;

namespace Framework.Core.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectsApplication _app;

        public ProjectsController(ProjectsApplication application)
        {
            _app = application;
        }

        [HttpGet]
        public async Task<IActionResult> GetListOfProjectsAsync(RangeQueryParameter parameters)
        {
            RangeQueryResult<ProjectInformation> listOfProjects = await _app.GetListOfAllProjects(parameters);
            return Ok(listOfProjects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectAsync(long id)
        {
            return Ok(await _app.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProjectAsync(CreateProjectParameter parameters)
        {
            ProjectInformation newProject = await _app.CreateProjectAsync(parameters);
            return Created(newProject.Id.ToString(), newProject.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyProjectAsync(long id, RenameProjectParameter parameters)
        {
            ProjectInformation targetProject = await _app.RenameProjectAsync(id, parameters);
            return Ok(targetProject);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectAsync(long id)
        {
            await _app.DeleteProjectAsync(id);
            return Ok();
        }
    }
}

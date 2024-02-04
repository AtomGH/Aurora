using Aurora.Core.Applications;
using Aurora.Library.Common;
using Aurora.Library.Projects;
using Microsoft.AspNetCore.Mvc;

namespace Framework.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectsApplication _app;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="application"></param>
        public ProjectsController(ProjectsApplication application)
        {
            _app = application;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetListOfProjectsAsync(RangeQueryParameter parameters)
        {
            RangeQueryResult<ProjectInformation> listOfProjects = await _app.GetListOfAllProjects(parameters);
            return Ok(listOfProjects);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectAsync(long id)
        {
            return Ok(await _app.GetAsync(id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateProjectAsync(CreateProjectParameters parameters)
        {
            ProjectInformation newProject = await _app.CreateProjectAsync(parameters);
            return Created(newProject.Id.ToString(), newProject.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyProjectAsync(long id, RenameProjectParameters parameters)
        {
            ProjectInformation targetProject = await _app.RenameProjectAsync(id, parameters);
            return Ok(targetProject);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectAsync(long id)
        {
            await _app.DeleteProjectAsync(id);
            return Ok();
        }
    }
}

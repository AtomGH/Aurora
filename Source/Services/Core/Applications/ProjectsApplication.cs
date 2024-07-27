using Aurora.Core.Data;
using Aurora.Core.Data.Entities;
using Aurora.Core.Applications.Extensions;
using Aurora.Library.Common;
using Aurora.Library.Projects;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Core.Applications
{
    /// <summary>
    /// Application layer that contains all bussiness logic to access projects.
    /// </summary>
    public class ProjectsApplication
    {
        private readonly DataService _data;

        /// <summary>
        /// Instantiate a project application.
        /// </summary>
        /// <param name="dataService"></param>
        public ProjectsApplication(DataService dataService)
        {
            _data = dataService;
        }

        /// <summary>
        /// Get all projects.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<RangeQueryResult<ProjectInformation>> GetListOfAllProjects(RangeQueryParameter parameters)
        {
            List<Project> listOfProjects = await _data.Database.Projects.Skip(parameters.Start - 1).Take(parameters.Limit).ToListAsync();
            List<ProjectInformation> listOfProjectInformations = new();
            listOfProjects.ForEach(p => listOfProjectInformations.Add(p.ToInformation()));
            long totalQuantity = await _data.Database.Projects.CountAsync();

            return new RangeQueryResult<ProjectInformation>(totalQuantity, listOfProjectInformations);
        }

        /// <summary>
        /// Get a specific project by ID.
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<ProjectInformation> GetAsync(int projectId)
        {
            Project targetProject = await _data.Projects.GetAsync(projectId);
            return targetProject.ToInformation();
        }

        /// <summary>
        /// Create a project.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<ProjectInformation> CreateProjectAsync(CreateProjectParameters parameters)
        {
            Account ownerAccount = await _data.Accounts.GetAsync(parameters.OwnerId);
            Project newProject = await _data.Projects.AddAsync(parameters.ProjectName, parameters.Description, parameters.Type, ownerAccount);
            await _data.SaveAsync();

            return newProject.ToInformation();
        }

        /// <summary>
        /// Rename a project.
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<ProjectInformation> RenameProjectAsync(int projectId, RenameProjectParameters parameters)
        {
            Project targetProject = await _data.Projects.GetAsync(projectId);
            targetProject.Name = parameters.NewName;
            await _data.SaveAsync();

            return targetProject.ToInformation();
        }

        /// <summary>
        /// Delete a project.
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task DeleteProjectAsync(int projectId)
        {
            await _data.Projects.RemoveAsync(projectId);
            await _data.SaveAsync();
        }
    }
}

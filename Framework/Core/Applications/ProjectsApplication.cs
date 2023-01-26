using Framework.Core.Data;
using Framework.Core.Data.Extensions;
using Framework.Core.Data.Models;
using Framework.Library.Common;
using Framework.Library.Projects;
using Microsoft.EntityFrameworkCore;

namespace Framework.Core.Applications
{
    public class ProjectsApplication
    {
        private readonly DataService _data;

        public ProjectsApplication(DataService dataService)
        {
            _data = dataService;
        }

        public async Task<RangeQueryResult<ProjectInformation>> GetListOfAllProjects(RangeQueryParameter parameters)
        {
            List<Project> listOfProjects = await _data.Projects.GetAsync(parameters.Start, parameters.Limit);
            List<ProjectInformation> listOfProjectInformations = new();
            listOfProjects.ForEach(p => listOfProjectInformations.Add(p.GetInformation()));
            long totalQuantity = await _data.Projects.CountAsync();

            return new RangeQueryResult<ProjectInformation>(totalQuantity, listOfProjectInformations);
        }

        public async Task<ProjectInformation> GetAsync(long projectId)
        {
            Project targetProject = await _data.Projects.GetAsync(projectId);
            return targetProject.GetInformation();
        }

        public async Task<ProjectInformation> CreateProjectAsync(CreateProjectParameter parameters)
        {
            Account ownerAccount = await _data.Accounts.GetAsync(parameters.OwnerId);
            Project newProject = await _data.Projects.AddAsync(parameters.ProjectName, parameters.Description, ownerAccount);
            await _data.SaveAsync();

            return newProject.GetInformation();
        }

        public async Task<ProjectInformation> RenameProjectAsync(long projectId, RenameProjectParameter parameters)
        {
            Project targetProject = await _data.Projects.GetAsync(projectId);
            targetProject.Name = parameters.NewName;
            await _data.SaveAsync();

            return targetProject.GetInformation();
        }

        public async Task DeleteProjectAsync(long projectId)
        {
            await _data.Projects.RemoveAsync(projectId);
            await _data.SaveAsync();
        }
    }
}

using Aurora.Core.Data.Entities;
using Aurora.Library.Common;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Core.Data.Services
{
    /// <summary>
    /// Project service.
    /// </summary>
    public class ProjectsService
    {
        private readonly DatabaseContext _context;
        private readonly IdentifierGenerator _idGenerator;

        /// <summary>
        /// Instantiate a project service.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="idGenerator"></param>
        public ProjectsService(DatabaseContext context, IdentifierGenerator idGenerator)
        {
            _context = context;
            _idGenerator = idGenerator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<Project> GetAsync(long projectId)
        {
            Project? targetProject = await _context.Projects.FindAsync(projectId);
            if (targetProject == null)
            {
                throw new InvalidOperationException("The project does not exist.");
            }
            return targetProject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="projectDescription"></param>
        /// <param name="projectOwner"></param>
        /// <returns></returns>
        public async Task<Project> AddAsync(string projectName, string projectDescription, Account projectOwner)
        {
            Project newProject = new(_context, _idGenerator.Get(), projectName, projectDescription, projectOwner);
            await _context.Projects.AddAsync(newProject);
            return newProject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task RemoveAsync(long projectId)
        {
            Project? targetProject = await _context.Projects.FindAsync(projectId);
            if (targetProject == null)
            {
                throw new InvalidOperationException("The project does not exist.");
            }
            if (targetProject.IsEmpty() == false)
            {
                throw new InvalidOperationException("The project is not empty and therefore cannot be deleted.");
            }
            _context.Projects.Remove(targetProject);
        }
    }
}

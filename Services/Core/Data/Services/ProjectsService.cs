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

        /// <summary>
        /// Instantiate a project service.
        /// </summary>
        /// <param name="context"></param>
        public ProjectsService(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of projects
        /// </summary>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<List<Project>> GetAsync(long start, int limit)
        {
            return await _context.Projects.LongSkip(start - 1).Take(limit).ToListAsync();
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
        /// <returns></returns>
        public async Task<long> CountAsync()
        {
            return await _context.Projects.LongCountAsync();
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
            Project newProject = new(_context, projectName, projectDescription, projectOwner);
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

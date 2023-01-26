using Framework.Core.Data.Models;
using Framework.Library.Common;
using Microsoft.EntityFrameworkCore;

namespace Framework.Core.Data.Services
{
    public class ProjectsService
    {
        private readonly DatabaseContext _context;

        public ProjectsService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Project>> GetAsync(long start, int limit)
        {
            return await _context.Projects.LongSkip(start - 1).Take(limit).ToListAsync();
        }

        public async Task<Project> GetAsync(long projectId)
        {
            Project? targetProject = await _context.Projects.FindAsync(projectId);
            if (targetProject == null)
            {
                throw new InvalidOperationException("The project does not exist.");
            }
            return targetProject;
        }

        public async Task<long> CountAsync()
        {
            return await _context.Projects.LongCountAsync();
        }

        public async Task<Project> AddAsync(string projectName, string projectDescription, Account projectOwner)
        {
            Project newProject = new(_context, projectName, projectDescription, projectOwner);
            await _context.Projects.AddAsync(newProject);
            return newProject;
        }

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

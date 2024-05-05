using Aurora.Core.Data.Entities;
using Aurora.Library.Common;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Core.Data.Services
{
    public class PipelinesService
    {
        private readonly DatabaseContext _context;

        public PipelinesService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Pipeline> GetPipelineAsync(long projectId, long pipelineId)
        {
            Pipeline? targetPipeline = await _context.Pipelines.Where(p => p.Id == pipelineId && p.Project.Id == projectId).FirstOrDefaultAsync();
            if (targetPipeline == null)
            {
                throw new InvalidOperationException("The pipeline does not exist.");
            }
            return targetPipeline;
        }

        public async Task<Pipeline> AddPipelineAsync(string name, string description, Project project)
        {
            Pipeline newPipeline = new(name, description, project);
            await _context.Pipelines.AddAsync(newPipeline);
            return newPipeline;
        }

        public async Task RemovePipelineAsync(long projectId, long pipelineId)
        {
            Pipeline? targetPipeline = await _context.Pipelines.Where(p => p.Id == pipelineId && p.Project.Id == projectId).FirstOrDefaultAsync();
            if (targetPipeline == null)
            {
                throw new InvalidOperationException("The pipeline does not exist.");
            }
            _context.Pipelines.Remove(targetPipeline);
        }
    }
}

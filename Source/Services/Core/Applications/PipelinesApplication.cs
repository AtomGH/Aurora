using Aurora.Core.Applications.Extensions;
using Aurora.Core.Data;
using Aurora.Core.Data.Entities;
using Aurora.Library.Common;
using Aurora.Library.Pipelines;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Core.Applications
{
    public class PipelinesApplication
    {
        private readonly DataService _data;

        public PipelinesApplication(DataService data)
        {
            _data = data;
        }

        public async Task<RangeQueryResult<PipelineInformation>> QueryPipelinesAsync(long projectId, RangeQueryParameter parameters)
        {
            List<Pipeline> pipelines = await _data.Database.Pipelines.Where(p => p.Project.Id == projectId).LongSkip(parameters.Start - 1).Take(parameters.Limit).ToListAsync();
            long totalQuantity = await _data.Database.Pipelines.Where(p => p.Project.Id == projectId).CountAsync();
            List<PipelineInformation> listOfInformations = new();
            foreach (Pipeline pipeline in pipelines)
            {
                listOfInformations.Add(pipeline.ToInformation());
            }
            return new RangeQueryResult<PipelineInformation>(totalQuantity, listOfInformations);
        }

        public async Task<PipelineInformation> GetPipelineAsync(long projectId, long pipelineId)
        {
            Pipeline? targetPipeline = await _data.Pipelines.GetPipelineAsync(projectId, pipelineId);
            return targetPipeline.ToInformation();
        }

        public async Task<PipelineInformation> CreatePipelineAsync(long projectId, CreatePipelineParameters parameters)
        {
            Project targetProject = await _data.Projects.GetAsync(projectId);
            Pipeline newPipeline = await _data.Pipelines.AddPipelineAsync(parameters.Name, parameters.Description, targetProject);
            await _data.SaveAsync();
            return newPipeline.ToInformation();
        }

        public async Task<PipelineInformation> UpdatePipelineAsync(long projectId, long pipelineId, CreatePipelineParameters parameters)
        {
            Pipeline targetPipeline = await _data.Pipelines.GetPipelineAsync(projectId, pipelineId);
            targetPipeline.Name = parameters.Name;
            targetPipeline.Description = parameters.Description;
            targetPipeline.Steps.Clear();
            foreach (KeyValuePair<int, string> kvp in parameters.Steps)
            {
                targetPipeline.Steps.Add(kvp.Key, kvp.Value);
            }
            await _data.SaveAsync();
            return targetPipeline.ToInformation();
        }

        public async Task DeletePipelineAsync(long projectId, long pipelineId)
        {
            await _data.Pipelines.RemovePipelineAsync(projectId, pipelineId);
            await _data.SaveAsync();
        }
    }
}

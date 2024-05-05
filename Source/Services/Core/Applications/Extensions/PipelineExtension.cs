using Aurora.Core.Data.Entities;
using Aurora.Library.Assets;
using Aurora.Library.Pipelines;

namespace Aurora.Core.Applications.Extensions
{
    public static class PipelineExtension
    {
        public static PipelineInformation ToInformation(this Pipeline pipeline)
        {
            return new()
            {
                Id = pipeline.Id,
                Name = pipeline.Name,
                Description = pipeline.Description,
                Steps = pipeline.Steps,
                ProjectId = pipeline.Project.Id,
            };
        }
    }
}

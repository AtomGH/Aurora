using Framework.Core.Data.Models;
using Framework.Library.Projects;

namespace Framework.Core.Data.Extensions
{
    public static class ProjectExtension
    {
        public static ProjectInformation GetInformation(this Project project)
        {
            return new()
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                OwnerId = project.Owner.Id
            };
        }
    }
}

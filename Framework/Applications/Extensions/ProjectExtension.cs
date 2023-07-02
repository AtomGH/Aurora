using Aurora.Framework.Data.Entities;
using Aurora.Library.Projects;

namespace Aurora.Framework.Applications.Extensions
{
    /// <summary>
    /// Extension methods for project at application level.
    /// </summary>
    public static class ProjectExtension
    {
        /// <summary>
        /// Convert the project to project information that does not contain any navigation properties.
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public static ProjectInformation ToInformation(this Project project)
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

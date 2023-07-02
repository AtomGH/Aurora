using Aurora.Framework.Data;

namespace Aurora.Framework.Applications
{
    /// <summary>
    /// Extension methods for the applications.
    /// </summary>
    public static class ApplicationsCollectionExtensions
    {
        /// <summary>
        /// Registry all the applications in the denpendency injection container.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplications(this IServiceCollection services)
        {
            services.AddScoped<AccountsApplication>();
            services.AddScoped<ProjectsApplication>();
            services.AddScoped<AssetsApplication>();
            
            return services;
        }
    }
}

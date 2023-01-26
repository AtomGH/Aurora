using Framework.Core.Data;

namespace Framework.Core.Applications
{
    public static class ApplicationsCollectionExtensions
    {
        public static IServiceCollection AddApplications(this IServiceCollection services)
        {
            services.AddScoped<AccountsApplication>();
            services.AddScoped<ProjectsApplication>();
            
            return services;
        }
    }
}

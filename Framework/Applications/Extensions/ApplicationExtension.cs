using Aurora.Framework.Data;

namespace Aurora.Framework.Applications.Extensions
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplications(this IServiceCollection services)
        {
            services.AddScoped<InstanceApplication>();
            services.AddSingleton<IdentifierApplication>();

            return services;
        }
    }
}

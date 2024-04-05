namespace Aurora.Services.Assets.Applications.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplications(this IServiceCollection services)
        {
            services.AddScoped<AssetsApplication>();
            services.AddScoped<AssetTypesApplication>();

            return services;
        }
    }
}

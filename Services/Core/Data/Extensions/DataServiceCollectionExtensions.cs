namespace Aurora.Core.Data.Extensions
{
    /// <summary>
    /// The extension methods for data services.
    /// </summary>
    public static class DataServiceCollectionExtensions
    {
        /// <summary>
        /// Register the class to dependency injection.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDataService(this IServiceCollection services)
        {
            services.AddScoped<DataService>();
            return services;
        }
    }
}

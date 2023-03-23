using Munters.Assignment.Services;

namespace Munters.Assignment.Extentions
{
    public static class ServiceExtention
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IGiphyService, GiphyService>();
            services.AddScoped<ICacheService, CacheService>();
        }

        public static void AppSettingsConfig(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<AppSettings>(config.GetSection("AppSettings"));
        }
    }
}

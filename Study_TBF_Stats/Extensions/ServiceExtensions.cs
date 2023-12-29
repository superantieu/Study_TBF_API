using Study_TBF_Stats.Contract;
using Study_TBF_Stats.Contract.IContract;


namespace Study_TBF_Stats.Extensions
{
    public static class ServiceExtensions
    {

        // CORS Configuration
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

        // IIS Configuration
        public static void ConfigureIISIntegration(this IServiceCollection service) =>
            service.Configure<IISOptions>(option =>
            {
            });

        // RepositoryManager Configuration
        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
             services.AddScoped<IRepositoryManager, RepositoryManager>();

        // ServiceManager Configuration
        public static void ConfigureServiceManager(this IServiceCollection services) =>
             services.AddScoped<IServiceManager, ServiceManager>();







    }
}

using Contracts;

using Entities;

using LoggerService;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using Repository;

namespace HomeLibraryAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureMsSqlContext(this IServiceCollection services)
        {
            services.AddDbContext<LibraryContext>();
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options => { });
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
    }
}

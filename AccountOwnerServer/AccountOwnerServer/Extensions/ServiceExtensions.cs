using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using AccountOwnerServer.Mappings;
using Microsoft.Extensions.Logging;
using LoggerService;
using Microsoft.Extensions.Configuration;
using Persistence;
using Microsoft.EntityFrameworkCore;
using Persistence.Contracts;
using Persistence.Repositories;
using LoggerService.Contracts;

namespace AccountOwnerServer.Extensions
{
    public static class ServiceExtensions
    {
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
            services.Configure<IISOptions>(options =>
            { });
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureOracleContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionString:Oracle"];

            services.AddDbContextPool<RepositoryContext>(opt => opt.UseOracle(connectionString));
        }

        public static void ConfigureRepositoryWrapper (this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(OwnerProfile));
        }

        public static void ConfigureLog(this IServiceCollection services)
            => services.AddSingleton<ILoggerManager, LoggerManager>();
    }
}

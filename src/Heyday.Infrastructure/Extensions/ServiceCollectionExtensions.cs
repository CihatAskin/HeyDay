using Heyday.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Heyday.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddServices();
        //services.AddApiVersioning(config =>
        //{
        //    config.DefaultApiVersion = new ApiVersion(1, 0);
        //    config.AssumeDefaultVersionWhenUnspecified = true;
        //    config.ReportApiVersions = true;
        //});
        services.AddDbContext<heydayContext>();
        return services;
    }
}
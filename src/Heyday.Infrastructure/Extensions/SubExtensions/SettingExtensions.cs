using Heyday.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Heyday.Infrastructure.Extensions.SubExtensions;

public static class SettingExtensions
{
    internal static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration config)
    {
        services
            .Configure<CorsSettings>(config.GetSection(nameof(CorsSettings)));
        return services;
    }
}
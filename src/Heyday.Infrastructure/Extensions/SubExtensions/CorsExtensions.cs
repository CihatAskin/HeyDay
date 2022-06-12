using Heyday.Application.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Heyday.Infrastructure.Extensions.SubExtensions;

public static class CorsExtensions
{
    internal static IServiceCollection AddCorsPolicy(this IServiceCollection services)
    {
        var corsSettings = services.GetOptions<CorsSettings>(nameof(CorsSettings));

        return services.AddCors(opt =>
            opt.AddPolicy("CorsPolicy", policy =>
                policy.AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials()
                      .WithOrigins(GetWithOrigins(corsSettings))));
    }

    private static string[] GetWithOrigins(CorsSettings corsSettings)
    {
        var result = new List<string>();
        if (corsSettings.React is not null)
        {
            result.AddRange(corsSettings.React.Split(';', StringSplitOptions.RemoveEmptyEntries));
        }

        return result.ToArray();
    }
}
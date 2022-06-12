using System.Runtime.CompilerServices;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

[assembly: InternalsVisibleTo("Heyday.Api.Main")]
namespace Heyday.Infrastructure.Extensions;

internal static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, IConfiguration config)
    {

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseCors("CorsPolicy");
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        
        });

     

        return app;
    }
}
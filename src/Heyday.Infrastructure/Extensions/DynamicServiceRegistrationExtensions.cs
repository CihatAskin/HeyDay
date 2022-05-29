using Heyday.Application.Common.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Heyday.Infrastructure.Extensions
{
    public static class DynamicServiceRegistrationExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            var transientServiceType = typeof(ITransientService);
            var transientServices = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => transientServiceType.IsAssignableFrom(p))
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Service = t.GetInterfaces().FirstOrDefault(),
                    Implementation = t
                })
                .Where(t => t.Service != null);

            foreach (var transientService in transientServices)
            {
                if (transientServiceType.IsAssignableFrom(transientService.Service))
                {
                    services.AddTransient(transientService.Service, transientService.Implementation);
                }
            }

            return services;
        }
    }
}

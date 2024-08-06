using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BaseProject.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayerServices(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

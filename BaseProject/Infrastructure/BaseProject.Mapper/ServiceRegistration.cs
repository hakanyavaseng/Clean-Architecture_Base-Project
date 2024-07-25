using Microsoft.Extensions.DependencyInjection;

namespace BaseProject.Mapper
{
    public static class ServiceRegistration
    {
        public static void AddCustomMapper(this IServiceCollection services)
        {
            services.AddSingleton<Application.Interfaces.Mapper.IMapper, AutoMapper.Mapper>();
        }
    }
}

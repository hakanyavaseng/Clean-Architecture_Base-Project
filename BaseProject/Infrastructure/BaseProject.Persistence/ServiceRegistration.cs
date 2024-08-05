using BaseProject.Application.Interfaces.Repositories;
using BaseProject.Application.Interfaces.Repositories.Common;
using BaseProject.Domain.Entities;
using BaseProject.Persistence.Contexts;
using BaseProject.Persistence.Repositories;
using BaseProject.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BaseProject.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureDbContext(services, configuration);
            ConfigureRepositories(services);
            ConfigureIdentity(services);
        }

        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BaseProjectDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();

            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentityCore<AppUser>().AddEntityFrameworkStores<BaseProjectDbContext>();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PoC.Infrastructure.EfCore;

namespace PoC.Infrastructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PoCDbContext>(opt =>
            {
                opt.UseNpgsql(configuration.GetConnectionString("PostgreSQL"));

                // This is for logging the queries in the console
                // Only for development environment!!
                opt.EnableSensitiveDataLogging();

                // This is for not tracking the entities
                //opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            });

            services.AddScoped<IPoCUnitOfWork, PoCUnitOfWork>();

            return services;
        }
    }
}

using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Extentions
{
    public static class EfCoreExtensions
    {
        public static IApplicationBuilder MigrateDatabase<TContext>(this IApplicationBuilder app, ILogger logger) where TContext : DbContext
        {
            using var scope = app.ApplicationServices.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();

            var databaseCreator = (RelationalDatabaseCreator)dbContext.Database.GetService<IDatabaseCreator>();

            logger.BeginScope("Migrating database {database}", dbContext.Database.GetDbConnection().Database);

            var isDbExists = databaseCreator.Exists();

            logger.LogInformation("Database exists: {isDbExists}", isDbExists);

            dbContext.Database.Migrate();

            logger.LogInformation("Database migrated successfully");

            return app;
        }
    }
}

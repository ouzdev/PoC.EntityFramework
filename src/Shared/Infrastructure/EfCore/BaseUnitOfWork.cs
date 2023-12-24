using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfCore
{
    public class BaseUnitOfWork<TContext>(TContext dbContext) : IUnitOfWork where TContext : DbContext
    {
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

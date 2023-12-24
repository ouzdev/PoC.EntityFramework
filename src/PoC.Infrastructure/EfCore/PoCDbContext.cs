using Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace PoC.Infrastructure.EfCore
{
    public class PoCDbContext(DbContextOptions<PoCDbContext> options) : BaseDbContext<Guid>(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PoCDbContext).Assembly);

            modelBuilder.HasDefaultSchema("poc");
        }
    }
}

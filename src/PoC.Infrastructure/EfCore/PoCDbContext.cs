using Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace PoC.Infrastructure.EfCore
{
    public class PoCDbContext : BaseDbContext<Guid>
    {
        public PoCDbContext(DbContextOptions<PoCDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PoCDbContext).Assembly);
        }
    }
}

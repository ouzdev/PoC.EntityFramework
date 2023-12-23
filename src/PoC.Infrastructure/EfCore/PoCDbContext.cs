using Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace PoC.Infrastructure.EfCore
{
    public class PoCDbContext : BaseDbContext<Guid>
    {
        public PoCDbContext(DbContextOptions<PoCDbContext> options) : base(options)
        {

        }
    }
}

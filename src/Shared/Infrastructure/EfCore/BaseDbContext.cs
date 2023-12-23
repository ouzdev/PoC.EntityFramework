using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfCore
{
    public class BaseDbContext<TKey>:DbContext where TKey : IEquatable<TKey>
    {
        public BaseDbContext(DbContextOptions options):base(options)
        {
            //Deneme
        }
    }
}

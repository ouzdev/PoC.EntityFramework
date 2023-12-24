using Infrastructure.EfCore;

namespace PoC.Infrastructure.EfCore
{
    public  class PoCUnitOfWork(PoCDbContext dbContext) : BaseUnitOfWork<PoCDbContext>(dbContext),IPoCUnitOfWork
    {
    }
}

using System.Threading;
using System.Threading.Tasks;
using TaskHub.Application.Common.Interfaces;
using TaskHub.Infrastructure.Persistence;

namespace TaskHub.Infrastructure.Persistence;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public EfUnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
 

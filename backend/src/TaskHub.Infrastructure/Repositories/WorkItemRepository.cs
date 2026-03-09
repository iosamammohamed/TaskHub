using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskHub.Application.Common.Interfaces;
using TaskHub.Application.Common.Models;
using TaskHub.Domain.Entities;
using TaskHub.Domain.Enums;
using TaskHub.Infrastructure.Persistence;

namespace TaskHub.Infrastructure.Repositories;

public class WorkItemRepository : BaseRepository<WorkItem>, IWorkItemRepository
{
    public WorkItemRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<PagedResult<WorkItem>> GetPagedAsync(int pageNumber, int pageSize, string? searchTerm, WorkItemStatus? status, int userId, CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsQueryable().Where(x => x.UserId == userId);


        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(x => x.Title.Contains(searchTerm) || (x.Description != null && x.Description.Contains(searchTerm)));
        }

        if (status.HasValue)
        {
            query = query.Where(x => x.Status == status.Value);
        }

        var totalCount = await query.CountAsync(cancellationToken);
        
        var items = await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<WorkItem>
        {
            Items = items,
            PageNumber = pageNumber,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }
}
 

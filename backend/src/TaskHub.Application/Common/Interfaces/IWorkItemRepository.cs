using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TaskHub.Domain.Entities;
using TaskHub.Domain.Enums;
using TaskHub.Application.Common.Models;

namespace TaskHub.Application.Common.Interfaces;

public interface IWorkItemRepository : IBaseRepository<WorkItem>
{
    Task<PagedResult<WorkItem>> GetPagedAsync(int pageNumber, int pageSize, string? searchTerm, WorkItemStatus? status, int userId, CancellationToken cancellationToken = default);

}
 

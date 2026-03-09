using TaskHub.Application.Common.Messaging;
using TaskHub.Application.Common.Models;
using TaskHub.Application.Features.WorkItems.DTOs;
using TaskHub.Domain.Enums;

namespace TaskHub.Application.Features.WorkItems.Queries.ListWorkItems;

public class ListWorkItemsQuery : PagedInput, IQuery<PagedResult<WorkItemListItemDto>>
{
    public string? SearchTerm { get; set; }
    public WorkItemStatus? Status { get; set; }
}
 

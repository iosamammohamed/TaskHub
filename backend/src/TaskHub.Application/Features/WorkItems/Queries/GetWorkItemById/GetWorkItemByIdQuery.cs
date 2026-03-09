using TaskHub.Application.Common.Messaging;
using TaskHub.Application.Features.WorkItems.DTOs;

namespace TaskHub.Application.Features.WorkItems.Queries.GetWorkItemById;

public class GetWorkItemByIdQuery : IQuery<WorkItemDto>
{
    public int Id { get; set; }
}
 

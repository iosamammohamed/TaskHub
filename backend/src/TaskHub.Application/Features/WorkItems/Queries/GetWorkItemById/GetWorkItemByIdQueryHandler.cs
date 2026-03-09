using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using TaskHub.Application.Common.Interfaces;
using TaskHub.Application.Features.WorkItems.DTOs;

namespace TaskHub.Application.Features.WorkItems.Queries.GetWorkItemById;

public class GetWorkItemByIdQueryHandler : IRequestHandler<GetWorkItemByIdQuery, WorkItemDto>
{
    private readonly IWorkItemRepository _repository;
    private readonly ICurrentUserService _currentUserService;

    public GetWorkItemByIdQueryHandler(IWorkItemRepository repository, ICurrentUserService currentUserService)
    {
        _repository = repository;
        _currentUserService = currentUserService;
    }

    public async Task<WorkItemDto> Handle(GetWorkItemByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        var currentUserId = _currentUserService.UserId ?? throw new System.UnauthorizedAccessException();

        if (entity == null || entity.UserId != currentUserId)
        {
            throw new System.Exception($"WorkItem with Id {request.Id} not found or you don't have access.");
        }


        return entity.Adapt<WorkItemDto>();

    }
}
 

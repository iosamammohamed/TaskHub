using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TaskHub.Application.Common.Interfaces;

namespace TaskHub.Application.Features.WorkItems.Commands.UpdateWorkItem;

public class UpdateWorkItemCommandHandler : IRequestHandler<UpdateWorkItemCommand, Unit>
{
    private readonly IWorkItemRepository _repository;
    private readonly ICurrentUserService _currentUserService;

    public UpdateWorkItemCommandHandler(IWorkItemRepository repository, ICurrentUserService currentUserService)
    {
        _repository = repository;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(UpdateWorkItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        var currentUserId = _currentUserService.UserId ?? throw new System.UnauthorizedAccessException();

        if (entity == null)
        {
            throw new System.Exception($"WorkItem with Id {request.Id} not found.");
        }

        if (entity.UserId != currentUserId)
        {
            throw new System.UnauthorizedAccessException("You do not have permission to update this work item.");
        }


        entity.UpdateDetails(request.Title, request.Description, request.Priority, request.DueDate);

        entity.ChangeStatus(request.Status);

        _repository.Update(entity);

        return Unit.Value;
    }
}

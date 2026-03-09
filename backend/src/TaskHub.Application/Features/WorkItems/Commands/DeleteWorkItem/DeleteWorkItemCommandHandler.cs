using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TaskHub.Application.Common.Interfaces;

namespace TaskHub.Application.Features.WorkItems.Commands.DeleteWorkItem;

public class DeleteWorkItemCommandHandler : IRequestHandler<DeleteWorkItemCommand, Unit>
{
    private readonly IWorkItemRepository _repository;
    private readonly ICurrentUserService _currentUserService;

    public DeleteWorkItemCommandHandler(IWorkItemRepository repository, ICurrentUserService currentUserService)
    {
        _repository = repository;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(DeleteWorkItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        var currentUserId = _currentUserService.UserId ?? throw new System.UnauthorizedAccessException();

        if (entity == null)
        {
            throw new System.Exception($"WorkItem with Id {request.Id} not found.");
        }

        if (entity.UserId != currentUserId)
        {
            throw new System.UnauthorizedAccessException("You do not have permission to delete this work item.");
        }


        _repository.Delete(entity);


        return Unit.Value;
    }
}
 

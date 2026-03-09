using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TaskHub.Application.Common.Interfaces;

namespace TaskHub.Application.Features.WorkItems.Commands.ChangeWorkItemStatus;

public class ChangeWorkItemStatusCommandHandler : IRequestHandler<ChangeWorkItemStatusCommand, Unit>
{
    private readonly IWorkItemRepository _repository;

    public ChangeWorkItemStatusCommandHandler(IWorkItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(ChangeWorkItemStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity == null)
        {
            throw new System.Exception($"WorkItem with Id {request.Id} not found.");
        }

        entity.ChangeStatus(request.Status);

        _repository.Update(entity);

        return Unit.Value;
    }
}
 

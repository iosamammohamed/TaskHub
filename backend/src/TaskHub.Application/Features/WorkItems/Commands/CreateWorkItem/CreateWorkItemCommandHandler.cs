using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TaskHub.Application.Common.Interfaces;
using TaskHub.Domain.Entities;

namespace TaskHub.Application.Features.WorkItems.Commands.CreateWorkItem;

public class CreateWorkItemCommandHandler : IRequestHandler<CreateWorkItemCommand, int>
{
    private readonly IWorkItemRepository _repository;
    private readonly ICurrentUserService _currentUserService;

    public CreateWorkItemCommandHandler(IWorkItemRepository repository, ICurrentUserService currentUserService)
    {
        _repository = repository;
        _currentUserService = currentUserService;
    }

    public async Task<int> Handle(CreateWorkItemCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId ?? throw new System.UnauthorizedAccessException();
        var workItem = new WorkItem(
            request.Title,
            request.Description,
            request.Priority,
            request.DueDate,
            userId);



        await _repository.AddAsync(workItem, cancellationToken);

        return workItem.Id;
    }
}

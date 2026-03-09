using TaskHub.Application.Common.Messaging;

namespace TaskHub.Application.Features.WorkItems.Commands.DeleteWorkItem;

public class DeleteWorkItemCommand : ICommand
{
    public int Id { get; set; }
}
 

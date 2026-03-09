using TaskHub.Application.Common.Messaging;
using TaskHub.Domain.Enums;

namespace TaskHub.Application.Features.WorkItems.Commands.ChangeWorkItemStatus;

public class ChangeWorkItemStatusCommand : ICommand
{
    public int Id { get; set; }
    public WorkItemStatus Status { get; set; }
}
 

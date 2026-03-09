using System;
using TaskHub.Application.Common.Messaging;
using TaskHub.Domain.Enums;

namespace TaskHub.Application.Features.WorkItems.Commands.UpdateWorkItem;

public class UpdateWorkItemCommand : ICommand
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public Priority Priority { get; set; }
    public WorkItemStatus Status { get; set; }
    public DateTime? DueDate { get; set; }
}

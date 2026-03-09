using System;
using TaskHub.Application.Common.Messaging;
using TaskHub.Domain.Enums;

namespace TaskHub.Application.Features.WorkItems.Commands.CreateWorkItem;

public class CreateWorkItemCommand : ICommand<int>
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public Priority Priority { get; set; }
    public DateTime? DueDate { get; set; }
}

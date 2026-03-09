using System;
using TaskHub.Domain.Enums;

namespace TaskHub.Application.Features.WorkItems.DTOs;

public class WorkItemListItemDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public WorkItemStatus Status { get; set; }
    public Priority Priority { get; set; }
    public DateTime? DueDate { get; set; }
}



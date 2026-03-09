using System;
using TaskHub.Domain.Enums;

namespace TaskHub.Application.Features.WorkItems.DTOs;

public class WorkItemDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public WorkItemStatus Status { get; set; }
    public Priority Priority { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime CreatedAt { get; set; }
}




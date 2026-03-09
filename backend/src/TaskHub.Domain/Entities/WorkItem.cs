using System;
using System.Collections.Generic;
using TaskHub.Domain.Common;
using TaskHub.Domain.Enums;
using TaskHub.Domain.Events;

namespace TaskHub.Domain.Entities;

public class WorkItem : Entity<int>, IAuditable, IHasDomainEvents
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }
    public WorkItemStatus Status { get; private set; }
    public Priority Priority { get; private set; }
    public DateTime? DueDate { get; private set; }

    public int UserId { get; private set; }
    public User Owner { get; private set; } = null!;


    // Auditing
    public DateTime CreatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public int? ModifiedBy { get; set; }



    // Domain Events
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    private WorkItem() { } // For EF

    public WorkItem(string title, string? description, Priority priority, DateTime? dueDate, int userId)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));

        Title = title;
        Description = description;
        Status = WorkItemStatus.New;
        Priority = priority;
        DueDate = dueDate;
        UserId = userId;

        _domainEvents.Add(new WorkItemCreatedEvent(this));
    }


    public void UpdateDetails(string title, string? description, Priority priority, DateTime? dueDate)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));

        Title = title;
        Description = description;
        Priority = priority;
        DueDate = dueDate;
    }

    public void ChangeStatus(WorkItemStatus newStatus)
    {
        if (Status != newStatus)
        {
            var oldStatus = Status;
            Status = newStatus;
            _domainEvents.Add(new WorkItemStatusChangedEvent(this, oldStatus));
        }
    }

    public void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    public void ClearDomainEvents() => _domainEvents.Clear();
}

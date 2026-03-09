using TaskHub.Domain.Common;
using TaskHub.Domain.Entities;
using TaskHub.Domain.Enums;

namespace TaskHub.Domain.Events;

public class WorkItemStatusChangedEvent : IDomainEvent
{
    public WorkItemStatusChangedEvent(WorkItem workItem, WorkItemStatus oldStatus)
    {
        WorkItem = workItem;
        OldStatus = oldStatus;
    }

    public WorkItem WorkItem { get; }
    public WorkItemStatus OldStatus { get; }
}

using TaskHub.Domain.Common;
using TaskHub.Domain.Entities;

namespace TaskHub.Domain.Events;

public class WorkItemCreatedEvent : IDomainEvent
{
    public WorkItemCreatedEvent(WorkItem workItem)
    {
        WorkItem = workItem;
    }

    public WorkItem WorkItem { get; }
}

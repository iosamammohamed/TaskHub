using MediatR;
using Microsoft.Extensions.Logging;
using TaskHub.Application.Common.Messaging;
using TaskHub.Domain.Events;

namespace TaskHub.Application.Features.WorkItems.EventHandlers;

public class WorkItemStatusChangedEventHandler : INotificationHandler<DomainEventNotification<WorkItemStatusChangedEvent>>
{
    private readonly ILogger<WorkItemStatusChangedEventHandler> _logger;

    public WorkItemStatusChangedEventHandler(ILogger<WorkItemStatusChangedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DomainEventNotification<WorkItemStatusChangedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;
        _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        // TODO: Handle WorkItemStatusChangedEvent (e.g., send notifications about status change)
        
        return Task.CompletedTask;
    }
}

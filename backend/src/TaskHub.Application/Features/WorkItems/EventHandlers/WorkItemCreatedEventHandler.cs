using MediatR;
using Microsoft.Extensions.Logging;
using TaskHub.Application.Common.Messaging;
using TaskHub.Domain.Events;

namespace TaskHub.Application.Features.WorkItems.EventHandlers;

public class WorkItemCreatedEventHandler : INotificationHandler<DomainEventNotification<WorkItemCreatedEvent>>
{
    private readonly ILogger<WorkItemCreatedEventHandler> _logger;

    public WorkItemCreatedEventHandler(ILogger<WorkItemCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DomainEventNotification<WorkItemCreatedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;
        _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        // TODO: Handle WorkItemCreatedEvent (e.g., send email, notify user, etc.)
        
        return Task.CompletedTask;
    }
}

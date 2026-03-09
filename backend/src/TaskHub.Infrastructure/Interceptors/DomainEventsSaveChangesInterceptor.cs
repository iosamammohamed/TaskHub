using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TaskHub.Domain.Common;

namespace TaskHub.Infrastructure.Interceptors;

public class DomainEventsSaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly IPublisher _publisher;

    public DomainEventsSaveChangesInterceptor(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await DispatchDomainEvents(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task DispatchDomainEvents(DbContext? context)
    {
        if (context == null) return;

        var entities = context.ChangeTracker
            .Entries<IHasDomainEvents>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity)
            .ToList();

        var domainEvents = entities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        entities.ForEach(e => e.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            // We use a generic notification wrapper usually, but MediatR can publish the event directly if it implements INotification
            // Or we can wrap it. The requirement mentioned Messaging/DomainEventNotification.cs
            // I'll use that wrapper if I want to be strictly according to plan, but simpler is to make events implement INotification.
            // Since I already created DomainEventNotification, I'll use it.
            
            var notificationType = typeof(TaskHub.Application.Common.Messaging.DomainEventNotification<>).MakeGenericType(domainEvent.GetType());
            var notification = System.Activator.CreateInstance(notificationType, domainEvent);
            
            if (notification != null)
                await _publisher.Publish(notification);
        }
    }
}
 

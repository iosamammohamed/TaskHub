using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TaskHub.Application.Common.Interfaces;
using TaskHub.Domain.Common;

namespace TaskHub.Infrastructure.Interceptors;

public class AuditingSaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly IDateTime _dateTime;
    private readonly ICurrentUserService _currentUserService;

    public AuditingSaveChangesInterceptor(IDateTime dateTime, ICurrentUserService currentUserService)
    {
        _dateTime = dateTime;
        _currentUserService = currentUserService;
    }


    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        foreach (var entry in context.ChangeTracker.Entries<IAuditable>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = _dateTime.Now;
                entry.Entity.CreatedBy = _currentUserService.UserId;
            }

            if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
            {
                entry.Entity.ModifiedAt = _dateTime.Now;
                entry.Entity.ModifiedBy = _currentUserService.UserId;
            }



        }
    }
}
 

using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Interceptors;

public class SoftDeleteInterceptor : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    { if (eventData.Context is null)
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        
        var entries = eventData.Context.ChangeTracker
            .Entries()
            .Where(e => e.State == EntityState.Deleted);
        
        foreach (var entry in entries)
        {
            entry.State = EntityState.Modified;
            if (entry.Entity is ISoftDeletable entity)
            {
                entity.Deactivate();
            }
        }
        
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
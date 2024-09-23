using System.Data;
using Application.Database;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure;

public class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
{
    public async Task<IDbTransaction> BeginTransaction(CancellationToken cancellationToken = default)
    {
        var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        return transaction.GetDbTransaction();
    }

    public async Task SaveChanges(CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
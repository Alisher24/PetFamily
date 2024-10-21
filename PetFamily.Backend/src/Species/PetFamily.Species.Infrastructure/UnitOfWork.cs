using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using PetFamily.Species.Application;
using PetFamily.Species.Infrastructure.DbContexts;

namespace PetFamily.Species.Infrastructure;

public class UnitOfWork(WriteDbContext dbContext) : IUnitOfWork
{
    public async Task<IDbTransaction> BeginTransaction(CancellationToken cancellationToken = default)
    {
        var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        return transaction.GetDbTransaction();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
using System.Data;

namespace PetFamily.Species.Application;

public interface IUnitOfWork
{
    Task<IDbTransaction> BeginTransaction(CancellationToken cancellationToken = default);
    
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
using PetFamily.SharedKernel.Shared;

namespace PetFamily.Core.Abstraction;

public interface IQueryService<TResponse, in TQuery> where TQuery : IQuery
{
    public Task<Result<TResponse>> ExecuteAsync(TQuery query, CancellationToken cancellationToken = default);
}
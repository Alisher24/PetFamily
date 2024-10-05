namespace Application.Abstraction;

public interface IQueryService<TResponse, in TQuery> where TQuery : IQuery
{
    public Task<TResponse> ExecuteAsync(TQuery query, CancellationToken cancellationToken = default);
}
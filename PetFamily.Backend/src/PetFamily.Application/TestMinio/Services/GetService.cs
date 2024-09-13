using Application.Providers;
using Application.TestMinio.Requests;
using Domain.Shared;

namespace Application.TestMinio.Services;

public class GetService(IFileProvider fileProvider)
{
    public async Task<Result<string>> ExecuteAsync(GetRequest request,
        CancellationToken cancellationToken = default)
    {
        return await fileProvider.GetFileAsync(request, cancellationToken);
    }
}
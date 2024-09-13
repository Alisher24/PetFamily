using Application.Providers;
using Application.TestMinio.Requests;
using Domain.Shared;

namespace Application.TestMinio.Services;

public class DeleteService(IFileProvider fileProvider)
{
    public async Task<Result<string>> ExecuteAsync(DeleteRequest request,
        CancellationToken cancellationToken = default)
    {
        return await fileProvider.DeleteFileAsync(request, cancellationToken);
    }
}
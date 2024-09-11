using Application.Providers;
using Application.TestMinio.Requests;
using Domain.Shared;

namespace Application.TestMinio.Services;

public class DeleteTestService(IFileProvider fileProvider)
{
    public async Task<Result<string>> ExecuteAsync(DeleteTestRequest request,
        CancellationToken cancellationToken = default)
    {
        return await fileProvider.DeleteFileAsync(request, cancellationToken);
    }
}
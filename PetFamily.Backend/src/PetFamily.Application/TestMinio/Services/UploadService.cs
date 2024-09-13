using Application.Providers;
using Application.TestMinio.Requests;
using Domain.Shared;

namespace Application.TestMinio.Services;

public class UploadService(IFileProvider fileProvider)
{
    public async Task<Result<string>> ExecuteAsync(UploadRequest request,
        CancellationToken cancellationToken = default)
    {
        return await fileProvider.UploadFileAsync(request, cancellationToken);
    }
}
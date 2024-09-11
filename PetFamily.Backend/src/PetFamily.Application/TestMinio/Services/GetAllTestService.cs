using Application.Providers;
using Domain.Shared;

namespace Application.TestMinio.Services;

public class GetAllTestService(IFileProvider fileProvider)
{
    public async Task<Result<List<string>>> ExecuteAsync(int expiry,
        CancellationToken cancellationToken = default)
    {
        return await fileProvider.GetAllFilesAsync(expiry, cancellationToken);
    }
}
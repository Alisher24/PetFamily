using Application.TestMinio.Requests;
using Domain.Shared;

namespace Application.Providers;

public interface IFileProvider
{
    Task<Result<string>> UploadFileAsync(UploadRequest request,
        CancellationToken cancellationToken = default);

    Task<Result<string>> GetFileAsync(GetRequest request,
        CancellationToken cancellationToken = default);

    Task<Result<List<string>>> GetAllFilesAsync(int expiry,
        CancellationToken cancellationToken = default);
    
    Task<Result<string>> DeleteFileAsync(DeleteRequest request,
        CancellationToken cancellationToken = default);
}
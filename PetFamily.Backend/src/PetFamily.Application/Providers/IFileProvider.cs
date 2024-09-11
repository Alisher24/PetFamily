using Application.TestMinio.Requests;
using Domain.Shared;

namespace Application.Providers;

public interface IFileProvider
{
    Task<Result<string>> UploadFileAsync(UploadTestRequest request,
        CancellationToken cancellationToken = default);

    Task<Result<string>> GetFileAsync(GetTestRequest request,
        CancellationToken cancellationToken = default);

    Task<Result<List<string>>> GetAllFilesAsync(int expiry,
        CancellationToken cancellationToken = default);
    
    Task<Result<string>> DeleteFileAsync(DeleteTestRequest request,
        CancellationToken cancellationToken = default);
}
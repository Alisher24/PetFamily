using Domain.Interfaces;
using Domain.Shared;

namespace Application.FileProvider;

public interface IFileProvider
{
    Task<Result<IReadOnlyList<IFilePath>>> UploadFileAsync(
        IEnumerable<FileData> fileData,
        CancellationToken cancellationToken = default);

    Task<Result<string>> GetFileAsync(GetFile file,
        CancellationToken cancellationToken = default);

    Task<Result<List<string>>> GetAllFilesAsync(int expiry,
        CancellationToken cancellationToken = default);

    Task<Result<string>> DeleteFileAsync(DeleteFile file,
        CancellationToken cancellationToken = default);
}
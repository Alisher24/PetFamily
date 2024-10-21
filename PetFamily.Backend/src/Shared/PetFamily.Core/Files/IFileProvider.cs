using PetFamily.SharedKernel.Interfaces;
using PetFamily.SharedKernel.Shared;

namespace PetFamily.Core.Files;

public interface IFileProvider
{
    Task<Result<IReadOnlyList<IFilePath>>> UploadFilesAsync(
        IEnumerable<FileData> fileData,
        CancellationToken cancellationToken = default);

    Task<Result<string>> GetFileAsync(GetFile file,
        CancellationToken cancellationToken = default);

    Task<Result<List<string>>> GetAllFilesAsync(int expiry,
        CancellationToken cancellationToken = default);

    Task<Result> RemoveFileAsync(FileInfo fileInfo,
        CancellationToken cancellationToken = default);
}
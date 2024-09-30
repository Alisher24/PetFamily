using Application.Files;
using Application.Messaging;
using Microsoft.Extensions.Logging;
using FileInfo = Application.Files.FileInfo;

namespace Infrastructure.Files;

public class FilesCleanerService(
    IFileProvider fileProvider,
    ILogger<FilesCleanerService> logger,
    IMessageQueue<IEnumerable<FileInfo>> messageQueue) : IFilesCleanerService
{
    public async Task Process(CancellationToken cancellationToken)
    {
        var fileInfos = await messageQueue.ReadAsync(cancellationToken);

        foreach (var fileInfo in fileInfos)
        {
            await fileProvider.RemoveFileAsync(fileInfo, cancellationToken);
            logger.LogInformation("Removed file with name {name} in bucket {bucket}", 
                fileInfo.FilePath, 
                fileInfo.BucketName);
        }
    }
}
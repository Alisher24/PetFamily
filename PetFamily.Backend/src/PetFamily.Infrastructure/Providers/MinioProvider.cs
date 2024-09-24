using Application.FileProvider;
using Domain.Interfaces;
using Domain.Shared;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.ApiEndpoints;
using Minio.DataModel.Args;

namespace Infrastructure.Providers;

public class MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger) : IFileProvider
{
    private const int MaxDegreeOfParallelism = 10;
    
    public async Task<Result<IReadOnlyList<IFilePath>>> UploadFileAsync(
        IEnumerable<FileData> fileData,
        CancellationToken cancellationToken = default)
    {
        var semaphoreSlim = new SemaphoreSlim(MaxDegreeOfParallelism);
        var filesList = fileData.ToList();
        
        try
        {
            await IfBucketsNotExistCreateBucket(filesList, cancellationToken);

            var tasks = filesList.Select(async file =>
                await PutObject(file, semaphoreSlim, cancellationToken));

            var pathResult = await Task.WhenAll(tasks);

            if (pathResult.Any(p => p.IsFailure))
                return pathResult.First(p => p.IsFailure).ErrorList;

            var results = pathResult.Select(p => p.Value).ToList();

            return results;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Fail to upload file in minio");
            return Error.Failure("file.upload", "Fail to upload file in minio");
        }
    }

    private async Task<Result<IFilePath>> PutObject(
        FileData fileData,
        SemaphoreSlim semaphoreSlim,
        CancellationToken cancellationToken)
    {
        await semaphoreSlim.WaitAsync(cancellationToken);

        var putObjectArgs = new PutObjectArgs()
            .WithBucket(fileData.BucketName)
            .WithStreamData(fileData.Stream)
            .WithObjectSize(fileData.Stream.Length)
            .WithObject(fileData.FilePath.Value);

        try
        {
            await minioClient
                .PutObjectAsync(putObjectArgs, cancellationToken);

            return Result<IFilePath>.Success(fileData.FilePath);
        }
        catch(Exception ex)
        {
            logger.LogError(ex,
                "Fail to upload file in minio with path {path} in bucket {bucket}",
                fileData.FilePath.Value,
                fileData.BucketName);

            return Error.Failure("file.upload", "Fail to upload file in minio");
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }
    
    private async Task IfBucketsNotExistCreateBucket(
        IEnumerable<FileData> filesData,
        CancellationToken cancellationToken)
    {
        HashSet<string> bucketNames = [..filesData.Select(file => file.BucketName)];

        foreach (var bucketName in bucketNames)
        {
            var bucketExistArgs = new BucketExistsArgs()
                .WithBucket(bucketName);

            var bucketExist = await minioClient
                .BucketExistsAsync(bucketExistArgs, cancellationToken);

            if (bucketExist == false)
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(bucketName);

                await minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
            }
        }
    }

    public async Task<Result<string>> GetFileAsync(
        GetFile file,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var getObjectArgs = new PresignedGetObjectArgs()
                .WithBucket(file.BucketName)
                .WithObject(file.ObjectName)
                .WithExpiry(file.Expiry);

            var url = await minioClient.PresignedGetObjectAsync(getObjectArgs);

            return url;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Fail to get file in minio");
            return Error.Failure("get.file", "Fail to get file in minio");
        }
    }

    public async Task<Result<List<string>>> GetAllFilesAsync(
        int expiry,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = new List<string>();

            var list = await minioClient.ListBucketsAsync(cancellationToken);
            foreach (var bucket in list.Buckets)
            {
                var args = new ListObjectsArgs()
                    .WithBucket(bucket.Name);

                var observable = minioClient.ListObjectsAsync(args);

                observable.Subscribe(async item =>
                {
                    var url = await GetFileAsync(
                        new GetFile(bucket.Name, item.Key, expiry), cancellationToken);
                    result.Add(url.Value);
                });
            }

            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Fail to get files in minio");
            return Error.Failure("get.files", "Fail to get files in minio");
        }
    }

    public async Task<Result<string>> DeleteFileAsync(
        DeleteFile file,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var rmArgs = new RemoveObjectArgs()
                .WithBucket(file.BucketName)
                .WithObject(file.ObjectName);

            await minioClient.RemoveObjectAsync(rmArgs, cancellationToken);

            logger.LogInformation("Removed element with name: {name}", file.BucketName);

            return file.BucketName;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Fail to remove file in minio");
            return Error.Failure("remove.file", "Fail to remove file in minio");
        }
    }
}
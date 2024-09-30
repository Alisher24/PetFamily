using Application.Files;
using Domain.Interfaces;
using Domain.Shared;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.ApiEndpoints;
using Minio.DataModel.Args;
using FileInfo = Application.Files.FileInfo;

namespace Infrastructure.Providers;

public class MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger) : IFileProvider
{
    private const int MaxDegreeOfParallelism = 10;

    public async Task<Result<IReadOnlyList<IFilePath>>> UploadFilesAsync(
        IEnumerable<FileData> fileData,
        CancellationToken cancellationToken = default)
    {
        var semaphoreSlim = new SemaphoreSlim(MaxDegreeOfParallelism);
        var filesList = fileData.ToList();

        try
        {
            await IfBucketsNotExistCreateBucketAsync(filesList.Select(file => file.Info.BucketName), cancellationToken);

            var tasks = filesList.Select(async file =>
                await PutObjectAsync(file, semaphoreSlim, cancellationToken));

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

    private async Task<Result<IFilePath>> PutObjectAsync(
        FileData fileData,
        SemaphoreSlim semaphoreSlim,
        CancellationToken cancellationToken)
    {
        await semaphoreSlim.WaitAsync(cancellationToken);

        var putObjectArgs = new PutObjectArgs()
            .WithBucket(fileData.Info.BucketName)
            .WithStreamData(fileData.Stream)
            .WithObjectSize(fileData.Stream.Length)
            .WithObject(fileData.Info.FilePath.Value);

        try
        {
            await minioClient
                .PutObjectAsync(putObjectArgs, cancellationToken);

            return Result<IFilePath>.Success(fileData.Info.FilePath);
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
                "Fail to upload file in minio with path {path} in bucket {bucket}",
                fileData.Info.FilePath.Value,
                fileData.Info.BucketName);

            return Error.Failure("file.upload", "Fail to upload file in minio");
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }

    private async Task IfBucketsNotExistCreateBucketAsync(
        IEnumerable<string> buckets,
        CancellationToken cancellationToken)
    {
        HashSet<string> bucketNames = [..buckets];

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

    public async Task<Result> RemoveFileAsync(
        FileInfo fileInfo,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var statArgs = new StatObjectArgs()
                .WithBucket(fileInfo.BucketName)
                .WithObject(fileInfo.FilePath.Value);

            var objectStat = await minioClient.StatObjectAsync(statArgs, cancellationToken);
            if (objectStat is null)
                return Result.Success();
            
            var rmArgs = new RemoveObjectArgs()
                .WithBucket(fileInfo.BucketName)
                .WithObject(fileInfo.FilePath.Value);

            await minioClient.RemoveObjectAsync(rmArgs, cancellationToken);

            logger.LogInformation("Removed element with name: {name}", fileInfo.BucketName);
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
                "Fail to remove file in minio with path {path} in bucket {bucket}",
                fileInfo.FilePath.Value,
                fileInfo.BucketName);
            return Error.Failure("remove.file", "Fail to remove file in minio");
        }

        return Result.Success();
    }
}
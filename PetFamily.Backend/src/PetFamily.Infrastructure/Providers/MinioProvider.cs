using Application.Providers;
using Application.TestMinio.Requests;
using Domain.Shared;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.ApiEndpoints;
using Minio.DataModel.Args;

namespace Infrastructure.Providers;

public class MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger) : IFileProvider
{
    public async Task<Result<string>> UploadFileAsync(
        UploadTestRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var bucketExistArgs = new BucketExistsArgs()
                .WithBucket(request.BucketName);

            var bucketExist = await minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken);
            if (bucketExist == false)
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(request.BucketName);

                await minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
            }

            var path = Guid.NewGuid();

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(request.BucketName)
                .WithStreamData(request.Stream)
                .WithObjectSize(request.Stream.Length)
                .WithObject(path.ToString());

            var result = await minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

            logger.LogInformation("File with objectName: {objectName} to upload", result.ObjectName);

            return result.ObjectName;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Fail to upload file in minio");
            return Error.Failure("file.upload", "Fail to upload file in minio");
        }
    }

    public async Task<Result<string>> GetFileAsync(
        GetTestRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var getObjectArgs = new PresignedGetObjectArgs()
                .WithBucket(request.BucketName)
                .WithObject(request.ObjectName)
                .WithExpiry(request.Expiry);

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
                        new GetTestRequest(bucket.Name, item.Key, expiry), cancellationToken);
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
        DeleteTestRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var rmArgs = new RemoveObjectArgs()
                .WithBucket(request.BucketName)
                .WithObject(request.ObjectName);

            await minioClient.RemoveObjectAsync(rmArgs, cancellationToken);

            logger.LogInformation("Removed element with name: {name}", request.BucketName);

            return request.BucketName;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Fail to remove file in minio");
            return Error.Failure("remove.file", "Fail to remove file in minio");
        }
    }
}
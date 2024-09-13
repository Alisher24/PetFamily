namespace Application.TestMinio.Requests;

public record UploadRequest(Stream Stream, string BucketName, string Path);
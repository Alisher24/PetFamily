namespace Application.TestMinio.Requests;

public record UploadTestRequest(Stream Stream, string BucketName, string ObjectName);
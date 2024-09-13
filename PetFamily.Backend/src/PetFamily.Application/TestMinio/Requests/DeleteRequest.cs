namespace Application.TestMinio.Requests;

public record DeleteRequest(string BucketName, string ObjectName);
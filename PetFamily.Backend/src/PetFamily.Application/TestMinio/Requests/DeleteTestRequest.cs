namespace Application.TestMinio.Requests;

public record DeleteTestRequest(string BucketName, string ObjectName);
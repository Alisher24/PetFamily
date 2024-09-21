namespace Application.TestMinio.Requests;

public record DeleteFile(string BucketName, string ObjectName);
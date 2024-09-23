namespace Application.TestMinio.Requests;

public record GetFile(string BucketName, string ObjectName, int Expiry);
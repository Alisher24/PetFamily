namespace Application.TestMinio.Requests;

public record GetRequest(string BucketName, string ObjectName, int Expiry);
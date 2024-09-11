namespace Application.TestMinio.Requests;

public record GetTestRequest(string BucketName, string ObjectName, int Expiry);
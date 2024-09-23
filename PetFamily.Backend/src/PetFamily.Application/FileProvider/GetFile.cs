namespace Application.FileProvider;

public record GetFile(string BucketName, string ObjectName, int Expiry);
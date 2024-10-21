namespace PetFamily.Core.Files;

public record GetFile(string BucketName, string ObjectName, int Expiry);
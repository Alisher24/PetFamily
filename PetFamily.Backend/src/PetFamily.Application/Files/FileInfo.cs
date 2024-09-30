using Domain.Interfaces;

namespace Application.Files;

public record FileInfo(IFilePath FilePath, string BucketName);
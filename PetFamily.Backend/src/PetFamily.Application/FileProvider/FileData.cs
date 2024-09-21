using Domain.Interfaces;

namespace Application.FileProvider;

public record FileData(Stream Stream, IFilePath FilePath, string BucketName);
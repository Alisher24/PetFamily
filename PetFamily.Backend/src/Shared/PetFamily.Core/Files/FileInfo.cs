using PetFamily.SharedKernel.Interfaces;

namespace PetFamily.Core.Files;

public record FileInfo(IFilePath FilePath, string BucketName);
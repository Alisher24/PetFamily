namespace PetFamily.Core.Files;

public interface IFilesCleanerService
{
    Task Process(CancellationToken cancellationToken);
}
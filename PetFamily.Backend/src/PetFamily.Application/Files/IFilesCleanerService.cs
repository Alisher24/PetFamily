namespace Application.Files;

public interface IFilesCleanerService
{
    Task Process(CancellationToken cancellationToken);
}
using Application.Abstraction;
using Application.Constants;
using Application.Database;
using Application.Extensions;
using Application.Messaging;
using Domain.Shared;
using FluentValidation;
using Microsoft.Extensions.Logging;
using FileInfo = Application.Files.FileInfo;

namespace Application.VolunteerManagement.Pets.Commands.DeletePetPhotos;

public class DeletePetPhotosService(
    IVolunteerRepository volunteerRepository,
    IValidator<DeletePetPhotosCommand> validator,
    ILogger<DeletePetPhotosService> logger,
    IMessageQueue<IEnumerable<FileInfo>> messageQueue,
    IUnitOfWork unitOfWork) : ICommandService<DeletePetPhotosCommand>
{
    public async Task<Result> ExecuteAsync(
        DeletePetPhotosCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        try
        {
            var volunteerResult = await volunteerRepository
                .GetByIdAsync(command.VolunteerId, cancellationToken);
            if (volunteerResult.IsFailure)
                return volunteerResult.ErrorList;

            var deletePetPhotosResult = volunteerResult.Value
                .DeletePetPhotos(command.PetId, command.PhotoPaths.ToList());
            if (deletePetPhotosResult.IsFailure)
                return deletePetPhotosResult.ErrorList;

            var filesInfo = deletePetPhotosResult.Value
                .Select(p => new FileInfo(p, MinIoConstants.PhotoBucketName));

            await unitOfWork.SaveChangesAsync(cancellationToken);

            await messageQueue.WriteAsync(filesInfo, cancellationToken);

            logger.LogInformation("Deleted photos from pet: {id}", command.PetId);

            return Result.Success();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to delete photo from pet: {id}", command.PetId);

            return Error.Failure($"Failed to delete photo from pet: {command.PetId}", "pet.photo.failure");
        }
    }
}
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Constants;
using PetFamily.Core.Extensions;
using PetFamily.Core.Messaging;
using PetFamily.SharedKernel.Shared;
using FileInfo = PetFamily.Core.Files.FileInfo;

namespace PetFamily.Volunteers.Application.Pets.Commands.DeletePetPhotos;

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
using Application.Abstraction;
using Application.Constants;
using Application.Database;
using Application.Extensions;
using Application.Messaging;
using Domain.Shared;
using FluentValidation;
using Microsoft.Extensions.Logging;
using FileInfo = Application.Files.FileInfo;

namespace Application.VolunteerManagement.Pets.Commands.Delete;

public class DeletePetService(
    IVolunteerRepository volunteerRepository,
    IValidator<DeletePetCommand> validator,
    ILogger<DeletePetService> logger,
    IMessageQueue<IEnumerable<FileInfo>> messageQueue,
    IUnitOfWork unitOfWork) : ICommandService<DeletePetCommand>
{
    public async Task<Result> ExecuteAsync(
        DeletePetCommand command, 
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerResult = await volunteerRepository
            .GetByIdAsync(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return Errors.General.NotFound($"Volunteer with id: {command.VolunteerId}");

        var deletePetResult = volunteerResult.Value.DeletePet(command.PetId);
        if (deletePetResult.IsFailure)
            return deletePetResult.ErrorList;

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        logger.LogInformation("Deleted pet with id {petId}", command.PetId);

        try
        {
            var filesInfo = deletePetResult.Value.PetPhotos
                .Select(p => new FileInfo(p.Path, MinIoConstants.PhotoBucketName));
            
            await messageQueue.WriteAsync(filesInfo, cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to delete photo from pet: {id}", command.PetId);

            return Error.Failure($"Failed to delete photo from pet: {command.PetId}", "pet.photo.failure");
        }
    }
}
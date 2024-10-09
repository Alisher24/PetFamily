using Application.Abstraction;
using Application.Database;
using Application.Extensions;
using Application.Messaging;
using Domain.Aggregates.Volunteer.ValueObjects;
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
    private const string BucketName = "photos";
    
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
            
            var petResult = volunteerResult.Value.GetPetById(command.PetId);
            if (petResult.IsFailure)
                return petResult.ErrorList;

            var petPhotos = new List<PetPhoto>();
            var filesInfo = new List<FileInfo>();
            foreach (var photoPath in command.PhotoPaths)
            {
                var petPhoto = petResult.Value.PetPhotos
                    .FirstOrDefault(p => p.Path.Value == photoPath);
                if (petPhoto is null)
                    return Errors.General
                        .NotFound($"Photo with path: {photoPath} of the pet with id: {command.PetId}");
                
                petPhotos.Add(petPhoto);
                filesInfo.Add(new FileInfo(petPhoto.Path, BucketName));
            }

            petResult.Value.DeletePhotos(petPhotos);

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
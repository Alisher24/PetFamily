using Application.Database;
using Application.Dtos;
using Application.Dtos.Validators;
using Application.Extensions;
using Application.FileProvider;
using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.Shared;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.VolunteerManagement.Pets.AddPetPhotos;

public class AddPetPhotosService(
    IFileProvider fileProvider,
    IVolunteerRepository volunteerRepository,
    IValidator<AddPetPhotosCommand> validator,
    ILogger<AddPetPhotosService> logger,
    IUnitOfWork unitOfWork)
{
    private const string BucketName = "photos";

    public async Task<Result> ExecuteAsync(
        AddPetPhotosCommand command,
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

            List<FileData> fileData = [];
            foreach (var photo in command.Photos)
            {
                var extension = Path.GetExtension(photo.FileName);

                var photoPath = PhotoPath.Create(Guid.NewGuid(), extension);
                if (photoPath.IsFailure)
                    return photoPath.ErrorList;

                var fileContent = new FileData(photo.Stream, photoPath.Value, BucketName);

                fileData.Add(fileContent);
            }

            var uploadResult = await fileProvider.UploadFileAsync(fileData, cancellationToken);
            if (uploadResult.IsFailure)
                return uploadResult.ErrorList;

            var petPhotos = uploadResult.Value
                .Select(f => new PetPhoto((PhotoPath)f, false))
                .ToList();

            petResult.Value.AddPhotos(petPhotos);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            logger.LogInformation("Uploaded photos to pet - {id}", petResult.Value.Id.Value);

            return Result.Success();
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
                "Can not upload photos to pet - {id} in transaction",
                command.PetId);

            return Error.Failure(
                $"Can not upload photos to pet - {command.PetId}",
                "pet.photo.failure");
        }
    }
}
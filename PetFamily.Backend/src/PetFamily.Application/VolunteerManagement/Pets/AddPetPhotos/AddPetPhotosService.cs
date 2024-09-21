using Application.Database;
using Application.Extensions;
using Application.FileProvider;
using Application.Providers;
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
        
        var transaction = await unitOfWork.BeginTransaction(cancellationToken);

        try
        {
            var volunteerResult = await volunteerRepository
                .GetByIdAsync(command.VolunteerId, cancellationToken);
            if (volunteerResult.IsFailure)
                return volunteerResult.ErrorList;

            var petResult = volunteerResult.Value.Pets
                .FirstOrDefault(p => p.Id.Value == command.PetId);
            if (petResult is null)
                return Errors.General.NotFound($"Pet with id: {command.PetId}");

            List<FileData> fileData = [];
            foreach (var photo in command.Photos)
            {
                var extension = Path.GetExtension(photo.FileName);

                var photoPath = PhotoPath.Create(Guid.NewGuid(), extension);
                if (photoPath.IsFailure)
                    return photoPath.ErrorList;

                var fileContent = new FileData(photo.Content, photoPath.Value, BucketName);

                fileData.Add(fileContent);
            }

            var petPhotos = fileData
                .Select(f => (PhotoPath)f.FilePath)
                .Select(f => new PetPhoto(f, false))
                .ToList();

            petResult.AddPhotos(petPhotos);

            await unitOfWork.SaveChanges(cancellationToken);

            var uploadResult = await fileProvider.UploadFileAsync(fileData, cancellationToken);
            if (uploadResult.IsFailure)
                return uploadResult.ErrorList;
        
            transaction.Commit();

            return Result.Success();
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
                "Can not add pet photos to pet - {id} in transaction",
                command.PetId);
            
            transaction.Rollback();

            return Error.Failure(
                $"Can not add pet photos to pet - {command.PetId}", 
                "pet.photo.failure");
        }
    }
}
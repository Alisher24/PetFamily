using Application.Abstraction;
using Application.Database;
using Application.Dtos;
using Application.Extensions;
using Application.Files;
using Application.Messaging;
using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.Shared;
using FluentValidation;
using Microsoft.Extensions.Logging;
using FileInfo = Application.Files.FileInfo;

namespace Application.VolunteerManagement.Pets.Commands.AddPetPhotos;

public class AddPetPhotosService(
    IFileProvider fileProvider,
    IVolunteerRepository volunteerRepository,
    IValidator<AddPetPhotosCommand> validator,
    ILogger<AddPetPhotosService> logger,
    IMessageQueue<IEnumerable<FileInfo>> messageQueue,
    IUnitOfWork unitOfWork) : ICommandService<AddPetPhotosCommand>
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

            var createFileDataResult = CreateFileData(command.Photos);
            if (createFileDataResult.IsFailure)
                return createFileDataResult.ErrorList;

            var uploadResult = await fileProvider.UploadFilesAsync(createFileDataResult.Value, cancellationToken);
            if (uploadResult.IsFailure)
            {
                await messageQueue.WriteAsync(createFileDataResult.Value
                    .Select(f => f.Info), cancellationToken);

                return uploadResult.ErrorList;
            }

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

            var createFileDataResult = CreateFileData(command.Photos);
            if (createFileDataResult.IsSuccess)
            {
                await messageQueue.WriteAsync(createFileDataResult.Value
                    .Select(f => f.Info), cancellationToken);
            }

            return Error.Failure(
                $"Can not upload photos to pet - {command.PetId}",
                "pet.photo.failure");
        }
    }

    private Result<List<FileData>> CreateFileData(IEnumerable<UploadFileDto> photos)
    {
        List<FileData> fileData = [];
        foreach (var photo in photos)
        {
            var extension = Path.GetExtension(photo.FileName);

            var photoPath = PhotoPath.Create(Path.GetFileName(photo.FileName), extension);
            if (photoPath.IsFailure)
                return photoPath.ErrorList;

            var fileContent = new FileData(photo.Stream, new FileInfo(photoPath.Value, BucketName));

            fileData.Add(fileContent);
        }

        return fileData;
    }
}
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel.Shared;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Application.Pets.Commands.AssignMainPetPhoto;

public class AssignMainPetPhotoService(
    IVolunteerRepository volunteerRepository,
    IValidator<AssignMainPetPhotoCommand> validator,
    ILogger<AssignMainPetPhotoService> logger,
    IUnitOfWork unitOfWork) : ICommandService<AssignMainPetPhotoCommand>
{
    public async Task<Result> ExecuteAsync(
        AssignMainPetPhotoCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerResult = await volunteerRepository
            .GetByIdAsync(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.ErrorList;

        var photoPath = PhotoPath.Create(
            Path.GetFileNameWithoutExtension(command.PhotoPath),
            Path.GetExtension(command.PhotoPath)).Value;

        var assignMainPetPhotoResult = volunteerResult.Value.AssignMainPetPhoto(command.PetId, photoPath);
        if (assignMainPetPhotoResult.IsFailure)
            return assignMainPetPhotoResult.ErrorList;

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        logger.LogInformation("Assigned main photo to pet - {id}", command.PetId);

        return Result.Success();
    }
}
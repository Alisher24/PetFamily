using FluentValidation;
using PetFamily.SharedKernel.Shared;

namespace PetFamily.Core.Dtos.Validators;

public class UploadFileDtoValidator : AbstractValidator<UploadFileDto>
{
    public long MaxLength { get; init; } = default!;
    public UploadFileDtoValidator()
    {
        RuleFor(c => c.FileName).NotEmpty().WithError(Errors.General.ValueIsInvalid());
        RuleFor(c => c.Stream).Must(c => c.Length < MaxLength);
    }
}
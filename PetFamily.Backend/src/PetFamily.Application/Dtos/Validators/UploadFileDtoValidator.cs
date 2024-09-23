using Domain.Shared;
using FluentValidation;

namespace Application.Dtos.Validators;

public class UploadFileDtoValidator : AbstractValidator<UploadFileDto>
{
    internal long MaxLength { get; init; } = default!;
    public UploadFileDtoValidator()
    {
        RuleFor(c => c.FileName).NotEmpty().WithError(Errors.General.ValueIsInvalid());
        RuleFor(c => c.Content).Must(c => c.Length < MaxLength);
    }
}
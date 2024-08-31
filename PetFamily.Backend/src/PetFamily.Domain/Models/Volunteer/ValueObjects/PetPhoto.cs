using CSharpFunctionalExtensions;
using Domain.Models.Shared;

namespace Domain.Models.Volunteer.ValueObjects;

public record PetPhoto
{
    private PetPhoto(string path, bool isMain)
    {
        Path = path;
        IsMain = isMain;
    }

    public string Path { get; } = default!;
    public bool IsMain { get; }

    public static Result<PetPhoto, Error> Create(string path, bool isMain)
    {
        if (string.IsNullOrWhiteSpace(path))
            return Errors.General.ValueIsInvalid("Path");

        return new PetPhoto(path, isMain);
    }
}
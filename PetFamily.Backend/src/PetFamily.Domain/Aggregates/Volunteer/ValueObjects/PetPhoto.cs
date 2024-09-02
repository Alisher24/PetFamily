using Domain.Shared;

namespace Domain.Aggregates.Volunteer.ValueObjects;

public record PetPhoto
{
    private PetPhoto(string path, bool isMain)
    {
        Path = path;
        IsMain = isMain;
    }

    public string Path { get; } = default!;
    public bool IsMain { get; }

    public static Result<PetPhoto> Create(string path, bool isMain)
    {
        if (string.IsNullOrWhiteSpace(path) || path.Length > Constants.MaxHighTextLenth)
            return Errors.General.ValueIsInvalid("Path");

        return new PetPhoto(path, isMain);
    }
}
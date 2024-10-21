namespace PetFamily.Volunteers.Domain.ValueObjects;

public record PetPhoto
{
    //ef core
    private PetPhoto()
    {
    }

    public PetPhoto(PhotoPath path, bool isMain)
    {
        Path = path;
        IsMain = isMain;
    }

    public PhotoPath Path { get; } = default!;
    public bool IsMain { get; }
}
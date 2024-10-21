using PetFamily.SharedKernel.Interfaces;
using PetFamily.SharedKernel.Shared;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Volunteers.Domain.ValueObjects;

public record PhotoPath : ValueObject<string>, IFilePath
{
    private const string Png = ".png";
    private const string Jpg = ".jpg";
    
    private PhotoPath(string value) : base(value)
    {
    }

    public static Result<PhotoPath> Create(string path, string extension)
    {
        if (extension != Png && extension != Jpg)
            return Errors.General.ValueIsInvalid("photo path");
        
        var fullPath = path + extension;

        return new PhotoPath(fullPath);
    }
}
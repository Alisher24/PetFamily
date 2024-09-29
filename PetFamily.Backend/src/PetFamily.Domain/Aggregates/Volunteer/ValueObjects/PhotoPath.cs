using Domain.CommonValueObjects;
using Domain.Interfaces;
using Domain.Shared;

namespace Domain.Aggregates.Volunteer.ValueObjects;

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
namespace PetFamily.Core.Dtos;

public record UpdateMainInfoDto(FullNameDto FullName,
    string Email,
    string Description,
    int YearsExperience,
    string PhoneNumber);
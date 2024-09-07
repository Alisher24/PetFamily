namespace Application.Volunteer.Dto;

public record UpdateMainInfoDto(FullNameDto FullName,
    string Email,
    string Description,
    int YearsExperience,
    string PhoneNumber);
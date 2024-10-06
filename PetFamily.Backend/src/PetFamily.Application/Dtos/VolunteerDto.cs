namespace Application.Dtos;

public class VolunteerDto
{
    public Guid Id { get; init; }

    public FullNameDto FullName { get; init; } = null!;

    public string Email { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public int YearsExperience { get; init; }

    public string PhoneNumber { get; init; } = string.Empty;

    public SocialNetworkDto[] SocialNetworks { get; init; } = null!;

    public RequisiteDto[] Requisites { get; init; } = null!;
    
    public bool IsDeleted { get; init; }
}
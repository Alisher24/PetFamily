using Domain.Interfaces;

namespace Domain.Models;

public class Volunteer : IEntityId<Guid>
{
    public Guid Id { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string Description { get; set; }
    public int YearsExperience { get; set; }
    public int CurrentPetsSeekingHome { get; } = 0;
    public int PetsAdopted { get; } = 0;
    public int PetsUnderTreatment { get; } = 0;
    public required string PhoneNumber { get; set; }
    public List<SocialNetwork> SocialNetworks { get; set; } = [];
    public List<Pet> Pets { get; set; } = [];
}
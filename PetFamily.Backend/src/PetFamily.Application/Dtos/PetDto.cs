namespace Application.Dtos;

public class PetDto
{
    public Guid Id { get; init; }
    
    public Guid VolunteerId { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public Guid SpeciesId { get; init; }

    public Guid BreedId { get; init; }

    public string Color { get; init; } = string.Empty;

    public string InformationHealth { get; init; } = string.Empty;

    public AddressDto Address { get; init; } = default!;

    public double Weight { get; init; }

    public double Height { get; init; }

    public string PhoneNumber { get; init; } = string.Empty;

    public bool IsNeutered { get; init; }

    public DateOnly DateOfBirth { get; init; }

    public bool IsVaccinated { get; init; }

    public int HelpStatus { get; init; }

    public DateTime CreatedAt { get; init; }

    public int Position { get; init; }

    public IEnumerable<RequisiteDto> Requisites { get; init; } = null!;

    public IEnumerable<PetPhotoDto> PetPhotos { get; init; } = null!;
    
    public bool IsDeleted { get; init; }
}
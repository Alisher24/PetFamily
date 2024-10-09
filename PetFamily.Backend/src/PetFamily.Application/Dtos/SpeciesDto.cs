namespace Application.Dtos;

public class SpeciesDto
{
    public Guid Id { get; init; }
    
    public string Name { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public List<BreedDto> Breeds { get; init; } = default!;
}
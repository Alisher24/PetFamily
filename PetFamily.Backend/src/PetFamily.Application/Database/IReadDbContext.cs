using Application.Dtos;

namespace Application.Database;

public interface IReadDbContext
{
    IQueryable<VolunteerDto> Volunteers { get; }
    
    IQueryable<PetDto> Pets { get; }
    
    IQueryable<SpeciesDto> Species { get; }

    IQueryable<BreedDto> Breeds { get; }
}
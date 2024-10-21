using PetFamily.Core.Dtos;

namespace PetFamily.Species.Application;

public interface IReadDbContext
{
    IQueryable<SpeciesDto> Species { get; }

    IQueryable<BreedDto> Breeds { get; }
    
    IQueryable<PetDto> Pets { get; }
}
using Application.Dtos;

namespace Application.Database;

public interface IReadDbContext
{
    IQueryable<VolunteerDto> Volunteers { get; }
    
    IQueryable<SpeciesDto> Species { get; }
}
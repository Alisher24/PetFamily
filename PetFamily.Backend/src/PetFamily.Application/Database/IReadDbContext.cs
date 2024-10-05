using System.Data.Entity;
using Application.Dtos;

namespace Application.Database;

public interface IReadDbContext
{
    DbSet<VolunteerDto> Volunteers { get; }
}
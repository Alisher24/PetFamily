using Application.SpeciesManagement.Breeds.Command;

namespace PetFamily.API.Controllers.Species.Requests;

public record AddBreedRequest(string Name, string Description)
{
    public AddBreedCommand ToCommand(Guid speciesId) => new(speciesId, Name, Description);
}
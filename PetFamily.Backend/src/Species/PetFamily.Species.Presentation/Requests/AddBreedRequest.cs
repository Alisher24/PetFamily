using PetFamily.Species.Application.Breeds.Command;

namespace PetFamily.Species.Presentation.Requests;

public record AddBreedRequest(string Name, string Description)
{
    public AddBreedCommand ToCommand(Guid speciesId) => new(speciesId, Name, Description);
}
using PetFamily.Species.Application.Species.Commands.Create;

namespace PetFamily.Species.Presentation.Requests;

public record CreateSpeciesRequest(string Name, string Description)
{
    public CreateSpeciesCommand ToCommand() => new(Name, Description);
}
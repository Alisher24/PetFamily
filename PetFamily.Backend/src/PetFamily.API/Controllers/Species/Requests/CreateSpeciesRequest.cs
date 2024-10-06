using Application.SpeciesManagement.Species.Commands.Create;

namespace PetFamily.API.Controllers.Species.Requests;

public record CreateSpeciesRequest(string Name, string Description)
{
    public CreateSpeciesCommand ToCommand() => new(Name, Description);
}
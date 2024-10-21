using PetFamily.Core.Abstraction;

namespace PetFamily.Species.Application.Species.Commands.Create;

public record CreateSpeciesCommand(string Name, string Description) : ICommand;
using Application.Abstraction;

namespace Application.SpeciesManagement.Species.Commands.Create;

public record CreateSpeciesCommand(string Name, string Description) : ICommand;
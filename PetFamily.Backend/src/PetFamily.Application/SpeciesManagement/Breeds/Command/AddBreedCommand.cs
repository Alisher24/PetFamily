using Application.Abstraction;

namespace Application.SpeciesManagement.Breeds.Command;

public record AddBreedCommand(Guid SpeciesId, string Name, string Description) : ICommand;
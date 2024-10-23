using PetFamily.Core.Abstraction;

namespace PetFamily.Species.Application.Breeds.Command;

public record AddBreedCommand(Guid SpeciesId, string Name, string Description) : ICommand;
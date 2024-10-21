using PetFamily.Core.Abstraction;

namespace PetFamily.Species.Application.Species.Commands.Delete;

public record DeleteSpeciesCommand(Guid Id) : ICommand;
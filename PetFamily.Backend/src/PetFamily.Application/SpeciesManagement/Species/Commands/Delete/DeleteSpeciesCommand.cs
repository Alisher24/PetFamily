using Application.Abstraction;

namespace Application.SpeciesManagement.Species.Commands.Delete;

public record DeleteSpeciesCommand(Guid Id) : ICommand;
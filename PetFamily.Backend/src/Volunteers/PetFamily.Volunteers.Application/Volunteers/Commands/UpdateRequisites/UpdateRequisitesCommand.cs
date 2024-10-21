using PetFamily.Core.Abstraction;
using PetFamily.Core.Dtos;

namespace PetFamily.Volunteers.Application.Volunteers.Commands.UpdateRequisites;

public record UpdateRequisitesCommand(Guid VolunteerId,
    IEnumerable<RequisiteDto> Requisites) : ICommand;
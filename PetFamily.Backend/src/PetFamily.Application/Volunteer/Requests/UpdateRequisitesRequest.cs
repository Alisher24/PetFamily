using Application.Volunteer.Dto;

namespace Application.Volunteer.Requests;

public record UpdateRequisitesRequest(Guid VolunteerId,
    IEnumerable<RequisiteDto> Requisites);
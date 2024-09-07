using Application.Volunteer.Dto;

namespace Application.Volunteer.Requests;

public record UpdateMainInfoRequest(Guid VolunteerId,
    UpdateMainInfoDto Dto);
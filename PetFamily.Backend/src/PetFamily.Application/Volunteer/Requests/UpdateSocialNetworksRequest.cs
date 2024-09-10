using Application.Volunteer.Dto;

namespace Application.Volunteer.Requests;

public record UpdateSocialNetworksRequest(Guid VolunteerId,
    IEnumerable<SocialNetworkDto> SocialNetworks);
using Application.Dtos;
using Application.VolunteerManagement.Volunteers.UpdateSocialNetworks;

namespace PetFamily.API.Controllers.Volunteers.Requests;

public record UpdateSocialNetworksRequest(
    Guid VolunteerId,
    IEnumerable<SocialNetworkDto> SocialNetworks)
{
    public UpdateSocialNetworksCommand ToCommand() =>
        new(VolunteerId, SocialNetworks);
}
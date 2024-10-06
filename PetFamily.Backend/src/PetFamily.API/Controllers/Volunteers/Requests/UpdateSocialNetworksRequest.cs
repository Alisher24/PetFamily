using Application.Dtos;
using Application.VolunteerManagement.Volunteers.Commands.UpdateSocialNetworks;

namespace PetFamily.API.Controllers.Volunteers.Requests;

public record UpdateSocialNetworksRequest(
    Guid VolunteerId,
    IEnumerable<SocialNetworkDto> SocialNetworks)
{
    public UpdateSocialNetworksCommand ToCommand() =>
        new(VolunteerId, SocialNetworks);
}
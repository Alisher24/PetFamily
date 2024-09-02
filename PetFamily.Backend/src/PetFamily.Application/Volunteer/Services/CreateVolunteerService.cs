using Application.Volunteer.Requests;
using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.Aggregates.Volunteer.ValueObjects.Ids;
using Domain.CommonFields;
using Domain.Shared;

namespace Application.Volunteer.Services;

public class CreateVolunteerService(IVolunteerRepository volunteerRepository)
{
    public async Task<Result<Guid>> ExecuteAsync(
        CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var email = Email.Create(request.Email);

        var phoneNumber = PhoneNumber.Create(request.PhoneNumber);

        var volunteerEmail = await volunteerRepository
            .GetByEmail(email.Value, cancellationToken);

        if (volunteerEmail.IsSuccess)
            return Errors.Volunteer.ValueIsAlreadyExists("email");

        var volunteerPhoneNumber = await volunteerRepository
            .GetByPhoneNumber(phoneNumber.Value, cancellationToken);

        if (volunteerPhoneNumber.IsSuccess)
            return Errors.Volunteer.ValueIsAlreadyExists("phone number");

        var volunteerId = VolunteerId.NewVolunteerId();

        var fullName = FullName.Create(request.FullName.FirstName,
            request.FullName.LastName,
            request.FullName.Patronymic);

        var description = Description.Create(request.Description);

        var socialNetworks = new SocialNetworkList(request.SocialNetworks
            .Select(s => new SocialNetwork(Name.Create(s.Name).Value,
                Link.Create(s.Link).Value)));

        var requisites = new RequisiteList(request.Requisites
            .Select(r => new Requisite(Name.Create(r.Name).Value,
                Description.Create(r.Description).Value)));

        var volunteer = new Domain.Aggregates.Volunteer.Volunteer(
            volunteerId,
            fullName.Value,
            email.Value,
            description.Value,
            request.YearsExperience,
            phoneNumber.Value,
            socialNetworks,
            requisites);

        await volunteerRepository.Add(volunteer, cancellationToken);

        return volunteer.Id.Value;
    }
}
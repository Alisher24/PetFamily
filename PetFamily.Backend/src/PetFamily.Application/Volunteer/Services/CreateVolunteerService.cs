using Application.Volunteer.Requests;
using CSharpFunctionalExtensions;
using Domain.Models.CommonFields;
using Domain.Models.Shared;
using Domain.Models.Volunteer.ValueObjects;
using Domain.Models.Volunteer.ValueObjects.Ids;

namespace Application.Volunteer.Services;

public class CreateVolunteerService(IVolunteerRepository volunteerRepository)
{
     private readonly IVolunteerRepository _volunteerRepository = volunteerRepository;

     public async Task<Result<Guid, Error>> ExecuteAsync(
          CreateVolunteerRequest request,
          CancellationToken cancellationToken = default)
     {
          var email = Email.Create(request.Email);

          if (email.IsFailure)
               return email.Error;
          
          var phoneNumber = PhoneNumber.Create(request.PhoneNumber);

          if (phoneNumber.IsFailure)
               return phoneNumber.Error;
          
          var volunteerEmail = await _volunteerRepository
               .GetByEmail(email.Value, cancellationToken);

          if (volunteerEmail.IsSuccess)
               return Errors.Volunteer.ValueIsAlreadyExists("email");

          var volunteerPhoneNumber = await _volunteerRepository
               .GetByPhoneNumber(phoneNumber.Value, cancellationToken);

          if (volunteerPhoneNumber.IsSuccess)
               return Errors.Volunteer.ValueIsAlreadyExists("phone number");
          
          var volunteerId = VolunteerId.NewVolunteerId();

          var fullName = FullName.Create(request.FullName.FirstName,
               request.FullName.LastName,
               request.FullName.Patronymic);

          if (fullName.IsFailure)
               return fullName.Error;

          var description = Description.Create(request.Description);

          if (description.IsFailure)
               return description.Error;

          var socialNetworks = request.SocialNetworks is null
               ? null
               : new SocialNetworkList(request.SocialNetworks
                    .Select(s => SocialNetwork
                         .Create(Name.Create(s.Name).Value, s.Link).Value));

          var requisites = request.Requisites is null
               ? null
               : new RequisiteList(request.Requisites
                    .Select(r => Requisite
                         .Create(Name.Create(r.Name).Value,
                              Description.Create(r.Description).Value).Value));

          var volunteer = new Domain.Models.Volunteer.Volunteer(
               volunteerId, 
               fullName.Value, 
               email.Value, 
               description.Value,
               request.YearsExperience,
               phoneNumber.Value,
               socialNetworks,
               requisites);

          await _volunteerRepository.Add(volunteer, cancellationToken);

          return volunteer.Id.Value;
     }
}
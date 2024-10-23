using PetFamily.SharedKernel.Shared;

namespace PetFamily.Volunteers.Domain.ValueObjects;

public record Address
{
    private Address(
        string city,
        string district,
        string street,
        string house)
    {
        City = city;
        District = district;
        Street = street;
        House = house;
    }

    public string City { get; } = default!;

    public string District { get; } = default!;

    public string Street { get; } = default!;

    public string House { get; } = default!;

    public static Result<Address> Create(
        string city,
        string district,
        string street,
        string house)
    {
        if (string.IsNullOrWhiteSpace(city))
            return Errors.General.ValueIsInvalid("City");

        if (string.IsNullOrWhiteSpace(district))
            return Errors.General.ValueIsInvalid("District");

        if (string.IsNullOrWhiteSpace(street))
            return Errors.General.ValueIsInvalid("Street");

        if (string.IsNullOrWhiteSpace(house))
            return Errors.General.ValueIsInvalid("House");

        return new Address(city, district, street, house);
    }
}
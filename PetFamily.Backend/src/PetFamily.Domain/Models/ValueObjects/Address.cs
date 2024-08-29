using CSharpFunctionalExtensions;

namespace Domain.Models.ValueObjects;

public record Address
{
    private Address(
        string county,
        string city,
        string district,
        string street,
        string house)
    {
        County = county;
        City = city;
        District = district;
        Street = street;
        House = house;
    }
    
    public string County { get; } = default!;

    public string City { get; } = default!;

    public string District { get; } = default!;

    public string Street { get; } = default!;

    public string House { get; } = default!;
    
    public static Result<Address> Create(
        string county, 
        string city, 
        string district, 
        string street,
        string house)
    {
        if (string.IsNullOrWhiteSpace(county))
            return Result.Failure<Address>("County cannot be empty");
        
        if (string.IsNullOrWhiteSpace(city))
            return Result.Failure<Address>("City cannot be empty");
        
        if (string.IsNullOrWhiteSpace(district))
            return Result.Failure<Address>("District cannot be empty");
        
        if (string.IsNullOrWhiteSpace(street))
            return Result.Failure<Address>("Street cannot be empty");
        
        if (string.IsNullOrWhiteSpace(house))
            return Result.Failure<Address>("House cannot be empty");

        return new Address(county, city, district, street, house);
    }
}
namespace Application.Dtos;

public record AddressDto(
    string City, 
    string District, 
    string Street,
    string House);
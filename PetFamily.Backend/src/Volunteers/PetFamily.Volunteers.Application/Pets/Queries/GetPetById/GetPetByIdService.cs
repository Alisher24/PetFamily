using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Dtos;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel.Shared;

namespace PetFamily.Volunteers.Application.Pets.Queries.GetPetById;

public class GetPetByIdService(
    IReadDbContext readDbContext,
    IValidator<GetPetByIdQuery> validator) : IQueryService<PetDto, GetPetByIdQuery>
{
    public async Task<Result<PetDto>> ExecuteAsync(
        GetPetByIdQuery query, 
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(query, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var petsQuery = readDbContext.Pets;

        var petResult = await readDbContext.Pets
            .FirstOrDefaultAsync(p => p.Id == query.Id, cancellationToken);
        if (petResult is null)
            return Errors.General.NotFound($"Pet with id: {query.Id}");

        return petResult;
    }
}
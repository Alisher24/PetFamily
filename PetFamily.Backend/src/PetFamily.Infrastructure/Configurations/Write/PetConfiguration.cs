using Application.Dtos;
using Domain.Aggregates.Species.ValueObjects.Ids;
using Domain.Aggregates.Volunteer.Entities;
using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.Aggregates.Volunteer.ValueObjects.Ids;
using Domain.CommonValueObjects;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Write;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(p => p.Id);

        //Id
        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => PetId.Create(value));

        //Name
        builder.ComplexProperty(p => p.Name, pb =>
        {
            pb.Property(n => n.Value)
                .IsRequired()
                .HasMaxLength(Domain.Shared.Constants.MaxLowTextLength)
                .HasColumnName("name");
        });

        //Description
        builder.ComplexProperty(p => p.Description, pb =>
        {
            pb.Property(d => d.Value)
                .IsRequired()
                .HasMaxLength(Domain.Shared.Constants.MaxHighTextLength)
                .HasColumnName("description");
        });

        //Color
        builder.ComplexProperty(p => p.Color, pb =>
        {
            pb.Property(c => c.Value)
                .IsRequired()
                .HasMaxLength(Domain.Shared.Constants.MaxLowTextLength)
                .HasColumnName("color");
        });

        //InformationHealth
        builder.ComplexProperty(p => p.InformationHealth, pb =>
        {
            pb.Property(i => i.Value)
                .IsRequired()
                .HasMaxLength(Domain.Shared.Constants.MaxHighTextLength)
                .HasColumnName("information_health");
        });

        //Address
        builder.OwnsOne(p => p.Address, pb =>
        {
            pb.ToJson("address");

            pb.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(Domain.Shared.Constants.MaxLowTextLength);

            pb.Property(a => a.District)
                .IsRequired()
                .HasMaxLength(Domain.Shared.Constants.MaxLowTextLength);

            pb.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(Domain.Shared.Constants.MaxLowTextLength);

            pb.Property(a => a.House)
                .IsRequired()
                .HasMaxLength(Domain.Shared.Constants.MaxLowTextLength);
        });

        //Weight
        builder.ComplexProperty(p => p.Weight, pb =>
        {
            pb.Property(w => w.Value)
                .IsRequired()
                .HasColumnName("weight");
        });

        //Height
        builder.ComplexProperty(p => p.Height, pb =>
        {
            pb.Property(h => h.Value)
                .IsRequired()
                .HasColumnName("height");
        });

        //PhoneNumber
        builder.ComplexProperty(p => p.PhoneNumber, pb =>
        {
            pb.Property(pn => pn.Value)
                .IsRequired()
                .HasMaxLength(Domain.Shared.Constants.MaxPhoneLength)
                .HasColumnName("phone_number");
        });

        //IsNeutered
        builder.Property(p => p.IsNeutered)
            .IsRequired();

        //DateOfBirth
        builder.ComplexProperty(p => p.DateOfBirth, pb =>
        {
            pb.Property(d => d.Value)
                .IsRequired()
                .HasColumnName("date_of_birth");
        });

        //HelpStatus
        builder.Property(p => p.HelpStatus)
            .IsRequired();

        //CreatedAt
        builder.Property(p => p.CreatedAt)
            .IsRequired();

        //Requisites
        builder.Property(p => p.Requisites)
            .ValueObjectsCollectionJsonConversion(
                r => new RequisiteDto(r.Name.Value, r.Description.Value),
                dto => new Requisite(Name.Create(dto.Name).Value, Description.Create(dto.Description).Value))
            .HasColumnName("requisites");

        //PetPhotos
        builder.Property(p => p.PetPhotos)
            .ValueObjectsCollectionJsonConversion(
                p => new PetPhotoDto(p.Path.Value, p.IsMain),
                dto => new PetPhoto(
                    PhotoPath.Create(
                        Path.GetFileNameWithoutExtension(dto.Path),
                        Path.GetExtension(dto.Path)).Value,
                    dto.IsMain))
            .HasColumnName("pet_photos");

        //Type
        builder.ComplexProperty(p => p.Type, pb =>
        {
            pb.Property(s => s.SpeciesId)
                .HasConversion(id => id.Value,
                    value => SpeciesId.Create(value))
                .IsRequired();

            pb.Property(b => b.BreedId)
                .IsRequired();
        });

        //Position
        builder.ComplexProperty(p => p.Position, pb =>
        {
            pb.Property(s => s.Value)
                .IsRequired()
                .HasColumnName("position");
        });

        //IsDeleted
        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
        
        builder.HasQueryFilter(p =>
            EF.Property<bool>(p, "_isDeleted") == false);
    }
}
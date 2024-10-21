using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Core.Dtos;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel.Shared;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.SharedKernel.ValueObjects.Ids;
using PetFamily.Volunteers.Domain;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Infrastructure.Configurations.Write;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");

        builder.HasKey(v => v.Id);

        //Id
        builder.Property(v => v.Id)
            .HasConversion(
                id => id.Value,
                value => VolunteerId.Create(value));

        //FullName
        builder.OwnsOne(v => v.FullName, vb =>
        {
            vb.ToJson("full_name");

            vb.Property(f => f.FirstName)
                .IsRequired()
                .HasMaxLength(Constants.MaxLowTextLength);

            vb.Property(f => f.LastName)
                .IsRequired()
                .HasMaxLength(Constants.MaxLowTextLength);

            vb.Property(f => f.Patronymic)
                .HasMaxLength(Constants.MaxLowTextLength);
        });

        //Email
        builder.ComplexProperty(v => v.Email, vb =>
        {
            vb.Property(e => e.Value)
                .IsRequired()
                .HasMaxLength(Constants.MaxEmailLength)
                .HasColumnName("email");
        });

        //Description
        builder.ComplexProperty(v => v.Description, vb =>
        {
            vb.Property(d => d.Value)
                .IsRequired()
                .HasMaxLength(Constants.MaxHighTextLength)
                .HasColumnName("description");
        });

        //YearsExperience
        builder.ComplexProperty(v => v.YearsExperience, vb =>
        {
            vb.Property(y => y.Value)
                .IsRequired()
                .HasColumnName("years_experience");
        });

        //PhoneNumber
        builder.ComplexProperty(v => v.PhoneNumber, vb =>
        {
            vb.Property(p => p.Value)
                .IsRequired()
                .HasMaxLength(Constants.MaxPhoneLength)
                .HasColumnName("phone_number");
        });

        //SocialNetworks
        builder.Property(v => v.SocialNetworks)
            .ValueObjectsCollectionJsonConversion(
                s => new SocialNetworkDto(s.Name.Value, s.Link.Value),
                dto => new SocialNetwork(Name.Create(dto.Name).Value, Link.Create(dto.Link).Value))
            .HasColumnName("social_networks");

        //Requisites
        builder.Property(v => v.Requisites)
            .ValueObjectsCollectionJsonConversion(
                r => new RequisiteDto(r.Name.Value, r.Description.Value),
                dto => new Requisite(Name.Create(dto.Name).Value, Description.Create(dto.Description).Value))
            .HasColumnName("requisites");

        //Pets
        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        //IsDeleted
        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");

        builder.HasQueryFilter(v =>
            EF.Property<bool>(v, "_isDeleted") == false);
    }
}
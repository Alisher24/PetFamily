using Application.Dtos;
using Domain.Aggregates.Volunteer;
using Domain.Aggregates.Volunteer.ValueObjects;
using Domain.Aggregates.Volunteer.ValueObjects.Ids;
using Domain.CommonValueObjects;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Write;

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
                .HasMaxLength(Domain.Shared.Constants.MaxLowTextLength);

            vb.Property(f => f.LastName)
                .IsRequired()
                .HasMaxLength(Domain.Shared.Constants.MaxLowTextLength);

            vb.Property(f => f.Patronymic)
                .HasMaxLength(Domain.Shared.Constants.MaxLowTextLength);
        });

        //Email
        builder.ComplexProperty(v => v.Email, vb =>
        {
            vb.Property(e => e.Value)
                .IsRequired()
                .HasMaxLength(Domain.Shared.Constants.MaxEmailLength)
                .HasColumnName("email");
        });

        //Description
        builder.ComplexProperty(v => v.Description, vb =>
        {
            vb.Property(d => d.Value)
                .IsRequired()
                .HasMaxLength(Domain.Shared.Constants.MaxHighTextLength)
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
                .HasMaxLength(Domain.Shared.Constants.MaxPhoneLength)
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
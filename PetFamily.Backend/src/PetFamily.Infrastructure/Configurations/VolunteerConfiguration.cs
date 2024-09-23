using Domain.Aggregates.Volunteer;
using Domain.Aggregates.Volunteer.ValueObjects.Ids;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

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
        builder.OwnsOne(v => v.SocialNetworks, vb =>
        {
            vb.ToJson("social_networks");

            vb.OwnsMany(l => l.Values, lb =>
            {
                lb.OwnsOne(s => s.Name, sb =>
                {
                    sb.Property(n => n.Value)
                        .IsRequired()
                        .HasMaxLength(Constants.MaxLowTextLength);
                });

                lb.OwnsOne(s => s.Link, sb =>
                {
                    sb.Property(l => l.Value)
                        .IsRequired()
                        .HasMaxLength(Constants.MaxHighTextLength);
                });
            });
        });
        
        //Requisites
        builder.OwnsOne(v => v.Requisites, vb =>
        {
            vb.ToJson("requisites");

            vb.OwnsMany(l => l.Values, lb =>
            {
                lb.OwnsOne(a => a.Name, ab =>
                {
                    ab.Property(n => n.Value)
                        .IsRequired()
                        .HasMaxLength(Constants.MaxLowTextLength);
                });

                lb.OwnsOne(a => a.Description, ab =>
                {
                    ab.Property(d => d.Value)
                        .IsRequired()
                        .HasMaxLength(Constants.MaxHighTextLength);
                });
            });
        });

        //Pets
        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id")
            .OnDelete(DeleteBehavior.Cascade);
        
        //IsDeleted
        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
    }
}
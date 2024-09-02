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
                .HasMaxLength(Constants.MaxLowTextLenth);

            vb.Property(f => f.LastName)
                .IsRequired()
                .HasMaxLength(Constants.MaxLowTextLenth);

            vb.Property(f => f.Patronymic)
                .HasMaxLength(Constants.MaxLowTextLenth);
        });

        //Email
        builder.ComplexProperty(v => v.Email, vb =>
        {
            vb.Property(e => e.Value)
                .IsRequired()
                .HasMaxLength(Constants.MaxEmailLenth)
                .HasColumnName("email");
        });
        
        //Description
        builder.ComplexProperty(v => v.Description, vb =>
        {
            vb.Property(d => d.Value)
                .IsRequired()
                .HasMaxLength(Constants.MaxHighTextLenth)
                .HasColumnName("description");
        });
        
        //YearsExperience
        builder.Property(v => v.YearsExperience)
            .IsRequired();
        
        //PhoneNumber
        builder.ComplexProperty(v => v.PhoneNumber, vb =>
        {
            vb.Property(p => p.Value)
                .IsRequired()
                .HasMaxLength(Constants.MaxPhoneLenth)
                .HasColumnName("phone_number");
        });

        //SocialNetworks
        builder.OwnsOne(v => v.SocialNetworks, vb =>
        {
            vb.ToJson("social_networks");

            vb.OwnsMany(l => l.SocialNetworks, lb =>
            {
                lb.OwnsOne(s => s.Name, sb =>
                {
                    sb.Property(n => n.Value)
                        .IsRequired()
                        .HasMaxLength(Constants.MaxLowTextLenth);
                });

                lb.OwnsOne(s => s.Link, sb =>
                {
                    sb.Property(l => l.Value)
                        .IsRequired()
                        .HasMaxLength(Constants.MaxHighTextLenth);
                });
            });
        });
        
        //Requisites
        builder.OwnsOne(v => v.Requisites, vb =>
        {
            vb.ToJson("requisites");

            vb.OwnsMany(l => l.Requisites, lb =>
            {
                lb.OwnsOne(a => a.Name, ab =>
                {
                    ab.Property(n => n.Value)
                        .IsRequired()
                        .HasMaxLength(Constants.MaxLowTextLenth);
                });

                lb.OwnsOne(a => a.Description, ab =>
                {
                    ab.Property(d => d.Value)
                        .IsRequired()
                        .HasMaxLength(Constants.MaxHighTextLenth);
                });
            });
        });

        //Pets
        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id");
    }
}
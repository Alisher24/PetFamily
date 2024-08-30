﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240830133844_AddSpeciesAndBreed")]
    partial class AddSpeciesAndBreed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.Breed", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid?>("species_id")
                        .HasColumnType("uuid")
                        .HasColumnName("species_id");

                    b.ComplexProperty<Dictionary<string, object>>("Description", "Domain.Models.Breed.Description#Description", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .HasColumnType("character varying(2000)")
                                .HasColumnName("description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Name", "Domain.Models.Breed.Name#Name", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("name");
                        });

                    b.HasKey("Id")
                        .HasName("pk_breeds");

                    b.HasIndex("species_id")
                        .HasDatabaseName("ix_breeds_species_id");

                    b.ToTable("breeds", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("color");

                    b.Property<string>("ContactPhoneNumber")
                        .IsRequired()
                        .HasMaxLength(19)
                        .HasColumnType("character varying(19)")
                        .HasColumnName("contact_phone_number");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date")
                        .HasColumnName("date_of_birth");

                    b.Property<double>("Height")
                        .HasColumnType("double precision")
                        .HasColumnName("height");

                    b.Property<int>("HelpStatus")
                        .HasColumnType("integer")
                        .HasColumnName("help_status");

                    b.Property<string>("InformationHealth")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("information_health");

                    b.Property<bool>("IsNeutered")
                        .HasColumnType("boolean")
                        .HasColumnName("is_neutered");

                    b.Property<bool>("IsVaccinated")
                        .HasColumnType("boolean")
                        .HasColumnName("is_vaccinated");

                    b.Property<double>("Weight")
                        .HasColumnType("double precision")
                        .HasColumnName("weight");

                    b.Property<Guid?>("volunteer_id")
                        .HasColumnType("uuid")
                        .HasColumnName("volunteer_id");

                    b.ComplexProperty<Dictionary<string, object>>("Description", "Domain.Models.Pet.Description#Description", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .HasColumnType("character varying(2000)")
                                .HasColumnName("description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Name", "Domain.Models.Pet.Name#Name", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("name");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Type", "Domain.Models.Pet.Type#Type", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<Guid>("BreedId")
                                .HasColumnType("uuid")
                                .HasColumnName("type_breed_id");

                            b1.Property<Guid>("SpeciesId")
                                .HasColumnType("uuid")
                                .HasColumnName("type_species_id");
                        });

                    b.HasKey("Id")
                        .HasName("pk_pets");

                    b.HasIndex("volunteer_id")
                        .HasDatabaseName("ix_pets_volunteer_id");

                    b.ToTable("pets", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Species", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.ComplexProperty<Dictionary<string, object>>("Description", "Domain.Models.Species.Description#Description", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .HasColumnType("character varying(2000)")
                                .HasColumnName("description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Name", "Domain.Models.Species.Name#Name", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("name");
                        });

                    b.HasKey("Id")
                        .HasName("pk_species");

                    b.ToTable("species", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Volunteer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("character varying(320)")
                        .HasColumnName("email");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(19)
                        .HasColumnType("character varying(19)")
                        .HasColumnName("phone_number");

                    b.Property<int>("YearsExperience")
                        .HasColumnType("integer")
                        .HasColumnName("years_experience");

                    b.ComplexProperty<Dictionary<string, object>>("Description", "Domain.Models.Volunteer.Description#Description", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .HasColumnType("character varying(2000)")
                                .HasColumnName("description");
                        });

                    b.HasKey("Id")
                        .HasName("pk_volunteers");

                    b.ToTable("volunteers", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Breed", b =>
                {
                    b.HasOne("Domain.Models.Species", null)
                        .WithMany("Breeds")
                        .HasForeignKey("species_id")
                        .HasConstraintName("fk_breeds_species_species_id");
                });

            modelBuilder.Entity("Domain.Models.Pet", b =>
                {
                    b.HasOne("Domain.Models.Volunteer", null)
                        .WithMany("Pets")
                        .HasForeignKey("volunteer_id")
                        .HasConstraintName("fk_pets_volunteers_volunteer_id");

                    b.OwnsOne("Domain.Models.ValueObjects.RequisiteList", "Requisites", b1 =>
                        {
                            b1.Property<Guid>("PetId")
                                .HasColumnType("uuid");

                            b1.HasKey("PetId")
                                .HasName("pk_pets");

                            b1.ToTable("pets");

                            b1.ToJson("requisites");

                            b1.WithOwner()
                                .HasForeignKey("PetId")
                                .HasConstraintName("fk_pets_pets_pet_id");

                            b1.OwnsMany("Domain.Models.ValueObjects.Requisite", "AssistanceDetails", b2 =>
                                {
                                    b2.Property<Guid>("RequisiteListPetId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.HasKey("RequisiteListPetId", "Id")
                                        .HasName("pk_pets");

                                    b2.ToTable("pets");

                                    b2.WithOwner()
                                        .HasForeignKey("RequisiteListPetId")
                                        .HasConstraintName("fk_pets_pets_requisite_list_pet_id");

                                    b2.OwnsOne("Domain.Models.CommonFields.Description", "Description", b3 =>
                                        {
                                            b3.Property<Guid>("RequisiteListPetId")
                                                .HasColumnType("uuid");

                                            b3.Property<int>("RequisiteId")
                                                .HasColumnType("integer");

                                            b3.Property<string>("Value")
                                                .IsRequired()
                                                .HasMaxLength(2000)
                                                .HasColumnType("character varying(2000)");

                                            b3.HasKey("RequisiteListPetId", "RequisiteId")
                                                .HasName("pk_pets");

                                            b3.ToTable("pets");

                                            b3.WithOwner()
                                                .HasForeignKey("RequisiteListPetId", "RequisiteId")
                                                .HasConstraintName("fk_pets_pets_requisite_list_pet_id_requisite_id");
                                        });

                                    b2.OwnsOne("Domain.Models.CommonFields.Name", "Name", b3 =>
                                        {
                                            b3.Property<Guid>("RequisiteListPetId")
                                                .HasColumnType("uuid");

                                            b3.Property<int>("RequisiteId")
                                                .HasColumnType("integer");

                                            b3.Property<string>("Value")
                                                .IsRequired()
                                                .HasMaxLength(100)
                                                .HasColumnType("character varying(100)");

                                            b3.HasKey("RequisiteListPetId", "RequisiteId")
                                                .HasName("pk_pets");

                                            b3.ToTable("pets");

                                            b3.WithOwner()
                                                .HasForeignKey("RequisiteListPetId", "RequisiteId")
                                                .HasConstraintName("fk_pets_pets_requisite_list_pet_id_requisite_id");
                                        });

                                    b2.Navigation("Description")
                                        .IsRequired();

                                    b2.Navigation("Name")
                                        .IsRequired();
                                });

                            b1.Navigation("AssistanceDetails");
                        });

                    b.OwnsOne("Domain.Models.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("PetId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)");

                            b1.Property<string>("County")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)");

                            b1.Property<string>("District")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)");

                            b1.Property<string>("House")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)");

                            b1.HasKey("PetId");

                            b1.ToTable("pets");

                            b1.ToJson("address");

                            b1.WithOwner()
                                .HasForeignKey("PetId")
                                .HasConstraintName("fk_pets_pets_id");
                        });

                    b.OwnsOne("Domain.Models.ValueObjects.PetPhotoList", "PetPhotos", b1 =>
                        {
                            b1.Property<Guid>("PetId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("PetId");

                            b1.ToTable("pets");

                            b1.ToJson("pet_photos");

                            b1.WithOwner()
                                .HasForeignKey("PetId")
                                .HasConstraintName("fk_pets_pets_id");

                            b1.OwnsMany("Domain.Models.ValueObjects.PetPhoto", "PetPhotos", b2 =>
                                {
                                    b2.Property<Guid>("PetPhotoListPetId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<bool>("IsMain")
                                        .HasColumnType("boolean");

                                    b2.Property<string>("Path")
                                        .IsRequired()
                                        .HasMaxLength(2000)
                                        .HasColumnType("character varying(2000)");

                                    b2.HasKey("PetPhotoListPetId", "Id")
                                        .HasName("pk_pets");

                                    b2.ToTable("pets");

                                    b2.WithOwner()
                                        .HasForeignKey("PetPhotoListPetId")
                                        .HasConstraintName("fk_pets_pets_pet_photo_list_pet_id");
                                });

                            b1.Navigation("PetPhotos");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("PetPhotos")
                        .IsRequired();

                    b.Navigation("Requisites")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.Volunteer", b =>
                {
                    b.OwnsOne("Domain.Models.ValueObjects.FullName", "FullName", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)");

                            b1.Property<string>("Patronymic")
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volunteers");

                            b1.ToJson("full_name");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteers_volunteers_id");
                        });

                    b.OwnsOne("Domain.Models.ValueObjects.SocialNetworkList", "SocialNetworks", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volunteers");

                            b1.ToJson("social_networks");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteers_volunteers_id");

                            b1.OwnsMany("Domain.Models.ValueObjects.SocialNetwork", "SocialNetworks", b2 =>
                                {
                                    b2.Property<Guid>("SocialNetworkListVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Link")
                                        .IsRequired()
                                        .HasMaxLength(2000)
                                        .HasColumnType("character varying(2000)");

                                    b2.HasKey("SocialNetworkListVolunteerId", "Id")
                                        .HasName("pk_volunteers");

                                    b2.ToTable("volunteers");

                                    b2.WithOwner()
                                        .HasForeignKey("SocialNetworkListVolunteerId")
                                        .HasConstraintName("fk_volunteers_volunteers_social_network_list_volunteer_id");

                                    b2.OwnsOne("Domain.Models.CommonFields.Name", "Name", b3 =>
                                        {
                                            b3.Property<Guid>("SocialNetworkListVolunteerId")
                                                .HasColumnType("uuid");

                                            b3.Property<int>("SocialNetworkId")
                                                .HasColumnType("integer");

                                            b3.Property<string>("Value")
                                                .IsRequired()
                                                .HasMaxLength(100)
                                                .HasColumnType("character varying(100)");

                                            b3.HasKey("SocialNetworkListVolunteerId", "SocialNetworkId")
                                                .HasName("pk_volunteers");

                                            b3.ToTable("volunteers");

                                            b3.WithOwner()
                                                .HasForeignKey("SocialNetworkListVolunteerId", "SocialNetworkId")
                                                .HasConstraintName("fk_volunteers_volunteers_social_network_list_volunteer_id_social_ne");
                                        });

                                    b2.Navigation("Name")
                                        .IsRequired();
                                });

                            b1.Navigation("SocialNetworks");
                        });

                    b.OwnsOne("Domain.Models.ValueObjects.RequisiteList", "Requisites", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volunteers");

                            b1.ToJson("requisites");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volunteers_volunteers_id");

                            b1.OwnsMany("Domain.Models.ValueObjects.Requisite", "AssistanceDetails", b2 =>
                                {
                                    b2.Property<Guid>("RequisiteListVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.HasKey("RequisiteListVolunteerId", "Id")
                                        .HasName("pk_volunteers");

                                    b2.ToTable("volunteers");

                                    b2.WithOwner()
                                        .HasForeignKey("RequisiteListVolunteerId")
                                        .HasConstraintName("fk_volunteers_volunteers_requisite_list_volunteer_id");

                                    b2.OwnsOne("Domain.Models.CommonFields.Description", "Description", b3 =>
                                        {
                                            b3.Property<Guid>("RequisiteListVolunteerId")
                                                .HasColumnType("uuid");

                                            b3.Property<int>("RequisiteId")
                                                .HasColumnType("integer");

                                            b3.Property<string>("Value")
                                                .IsRequired()
                                                .HasMaxLength(2000)
                                                .HasColumnType("character varying(2000)");

                                            b3.HasKey("RequisiteListVolunteerId", "RequisiteId")
                                                .HasName("pk_volunteers");

                                            b3.ToTable("volunteers");

                                            b3.WithOwner()
                                                .HasForeignKey("RequisiteListVolunteerId", "RequisiteId")
                                                .HasConstraintName("fk_volunteers_volunteers_requisite_list_volunteer_id_requisite_id");
                                        });

                                    b2.OwnsOne("Domain.Models.CommonFields.Name", "Name", b3 =>
                                        {
                                            b3.Property<Guid>("RequisiteListVolunteerId")
                                                .HasColumnType("uuid");

                                            b3.Property<int>("RequisiteId")
                                                .HasColumnType("integer");

                                            b3.Property<string>("Value")
                                                .IsRequired()
                                                .HasMaxLength(100)
                                                .HasColumnType("character varying(100)");

                                            b3.HasKey("RequisiteListVolunteerId", "RequisiteId")
                                                .HasName("pk_volunteers");

                                            b3.ToTable("volunteers");

                                            b3.WithOwner()
                                                .HasForeignKey("RequisiteListVolunteerId", "RequisiteId")
                                                .HasConstraintName("fk_volunteers_volunteers_requisite_list_volunteer_id_requisite_id");
                                        });

                                    b2.Navigation("Description")
                                        .IsRequired();

                                    b2.Navigation("Name")
                                        .IsRequired();
                                });

                            b1.Navigation("AssistanceDetails");
                        });

                    b.Navigation("FullName")
                        .IsRequired();

                    b.Navigation("Requisites")
                        .IsRequired();

                    b.Navigation("SocialNetworks")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.Species", b =>
                {
                    b.Navigation("Breeds");
                });

            modelBuilder.Entity("Domain.Models.Volunteer", b =>
                {
                    b.Navigation("Pets");
                });
#pragma warning restore 612, 618
        }
    }
}

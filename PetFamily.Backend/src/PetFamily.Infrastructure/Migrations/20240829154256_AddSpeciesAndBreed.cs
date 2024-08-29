﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSpeciesAndBreed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "breed",
                table: "pets");

            migrationBuilder.RenameColumn(
                name: "nickname",
                table: "pets",
                newName: "name");

            migrationBuilder.AddColumn<Guid>(
                name: "breed_id",
                table: "pets",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "species_id",
                table: "pets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "species",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_species", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "breeds",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    species_id = table.Column<Guid>(type: "uuid", nullable: true),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_breeds", x => x.id);
                    table.ForeignKey(
                        name: "fk_breeds_species_species_id",
                        column: x => x.species_id,
                        principalTable: "species",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_pets_breed_id",
                table: "pets",
                column: "breed_id");

            migrationBuilder.CreateIndex(
                name: "ix_pets_species_id",
                table: "pets",
                column: "species_id");

            migrationBuilder.CreateIndex(
                name: "ix_breeds_species_id",
                table: "breeds",
                column: "species_id");

            migrationBuilder.AddForeignKey(
                name: "fk_pets_breeds_breed_id",
                table: "pets",
                column: "breed_id",
                principalTable: "breeds",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_pets_species_species_id",
                table: "pets",
                column: "species_id",
                principalTable: "species",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_pets_breeds_breed_id",
                table: "pets");

            migrationBuilder.DropForeignKey(
                name: "fk_pets_species_species_id",
                table: "pets");

            migrationBuilder.DropTable(
                name: "breeds");

            migrationBuilder.DropTable(
                name: "species");

            migrationBuilder.DropIndex(
                name: "ix_pets_breed_id",
                table: "pets");

            migrationBuilder.DropIndex(
                name: "ix_pets_species_id",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "breed_id",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "species_id",
                table: "pets");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "pets",
                newName: "nickname");

            migrationBuilder.AddColumn<string>(
                name: "breed",
                table: "pets",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}

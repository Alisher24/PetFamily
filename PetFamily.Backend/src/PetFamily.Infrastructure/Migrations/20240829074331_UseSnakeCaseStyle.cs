using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UseSnakeCaseStyle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SocialNetworks",
                table: "volunteers",
                newName: "social_networks");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "volunteers",
                newName: "full_name");

            migrationBuilder.RenameColumn(
                name: "AssistanceDetails",
                table: "volunteers",
                newName: "assistance_details");

            migrationBuilder.RenameColumn(
                name: "PetPhotos",
                table: "pets",
                newName: "pet_photos");

            migrationBuilder.RenameColumn(
                name: "AssistanceDetails",
                table: "pets",
                newName: "assistance_details");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "social_networks",
                table: "volunteers",
                newName: "SocialNetworks");

            migrationBuilder.RenameColumn(
                name: "full_name",
                table: "volunteers",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "assistance_details",
                table: "volunteers",
                newName: "AssistanceDetails");

            migrationBuilder.RenameColumn(
                name: "pet_photos",
                table: "pets",
                newName: "PetPhotos");

            migrationBuilder.RenameColumn(
                name: "assistance_details",
                table: "pets",
                newName: "AssistanceDetails");
        }
    }
}

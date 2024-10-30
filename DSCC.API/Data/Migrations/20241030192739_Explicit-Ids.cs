using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSCC.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExplicitIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdoptionId",
                table: "Pets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PetId",
                table: "Adoptions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdoptionId",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "PetId",
                table: "Adoptions");
        }
    }
}

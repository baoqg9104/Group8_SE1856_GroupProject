using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayers.Migrations
{
    /// <inheritdoc />
    public partial class _19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndMonth",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "EndYear",
                table: "Educations");

            migrationBuilder.RenameColumn(
                name: "StartYear",
                table: "Educations",
                newName: "FromYear");

            migrationBuilder.RenameColumn(
                name: "StartMonth",
                table: "Educations",
                newName: "FromMonth");

            migrationBuilder.AddColumn<int>(
                name: "ToMonth",
                table: "Educations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ToYear",
                table: "Educations",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToMonth",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "ToYear",
                table: "Educations");

            migrationBuilder.RenameColumn(
                name: "FromYear",
                table: "Educations",
                newName: "StartYear");

            migrationBuilder.RenameColumn(
                name: "FromMonth",
                table: "Educations",
                newName: "StartMonth");

            migrationBuilder.AddColumn<int>(
                name: "EndMonth",
                table: "Educations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EndYear",
                table: "Educations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

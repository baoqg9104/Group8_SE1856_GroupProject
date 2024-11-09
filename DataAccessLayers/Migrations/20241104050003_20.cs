using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayers.Migrations
{
    /// <inheritdoc />
    public partial class _20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndMonth",
                table: "WorkExperiences");

            migrationBuilder.DropColumn(
                name: "EndYear",
                table: "WorkExperiences");

            migrationBuilder.RenameColumn(
                name: "StartYear",
                table: "WorkExperiences",
                newName: "FromYear");

            migrationBuilder.RenameColumn(
                name: "StartMonth",
                table: "WorkExperiences",
                newName: "FromMonth");

            migrationBuilder.AddColumn<int>(
                name: "ToMonth",
                table: "WorkExperiences",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ToYear",
                table: "WorkExperiences",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToMonth",
                table: "WorkExperiences");

            migrationBuilder.DropColumn(
                name: "ToYear",
                table: "WorkExperiences");

            migrationBuilder.RenameColumn(
                name: "FromYear",
                table: "WorkExperiences",
                newName: "StartYear");

            migrationBuilder.RenameColumn(
                name: "FromMonth",
                table: "WorkExperiences",
                newName: "StartMonth");

            migrationBuilder.AddColumn<int>(
                name: "EndMonth",
                table: "WorkExperiences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EndYear",
                table: "WorkExperiences",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

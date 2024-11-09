using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayers.Migrations
{
    /// <inheritdoc />
    public partial class _13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Location",
                table: "RecruitmentCompanies",
                newName: "Country");

            migrationBuilder.AddColumn<string>(
                name: "CompanyType",
                table: "RecruitmentCompanies",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyType",
                table: "RecruitmentCompanies");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "RecruitmentCompanies",
                newName: "Location");
        }
    }
}

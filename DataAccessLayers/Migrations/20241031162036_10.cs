using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayers.Migrations
{
    /// <inheritdoc />
    public partial class _10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_RecruitmentCompanies_CompanyId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_RecruitmentCompanies_CompanyId",
                table: "Users",
                column: "CompanyId",
                principalTable: "RecruitmentCompanies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_RecruitmentCompanies_CompanyId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_RecruitmentCompanies_CompanyId",
                table: "Users",
                column: "CompanyId",
                principalTable: "RecruitmentCompanies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

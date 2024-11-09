using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayers.Migrations
{
    /// <inheritdoc />
    public partial class _9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecruitmentCompanies_Users_UserId",
                table: "RecruitmentCompanies");

            migrationBuilder.DropIndex(
                name: "IX_RecruitmentCompanies_UserId",
                table: "RecruitmentCompanies");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "RecruitmentCompanies");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyId",
                table: "Users",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_RecruitmentCompanies_CompanyId",
                table: "Users",
                column: "CompanyId",
                principalTable: "RecruitmentCompanies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_RecruitmentCompanies_CompanyId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CompanyId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "RecruitmentCompanies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RecruitmentCompanies_UserId",
                table: "RecruitmentCompanies",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RecruitmentCompanies_Users_UserId",
                table: "RecruitmentCompanies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayers.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_RecruitmentCompanys_CompanyId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_RecruitmentCompanys_Users_UserId",
                table: "RecruitmentCompanys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecruitmentCompanys",
                table: "RecruitmentCompanys");

            migrationBuilder.RenameTable(
                name: "RecruitmentCompanys",
                newName: "RecruitmentCompanies");

            migrationBuilder.RenameIndex(
                name: "IX_RecruitmentCompanys_UserId",
                table: "RecruitmentCompanies",
                newName: "IX_RecruitmentCompanies_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecruitmentCompanies",
                table: "RecruitmentCompanies",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_RecruitmentCompanies_CompanyId",
                table: "Jobs",
                column: "CompanyId",
                principalTable: "RecruitmentCompanies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecruitmentCompanies_Users_UserId",
                table: "RecruitmentCompanies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_RecruitmentCompanies_CompanyId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_RecruitmentCompanies_Users_UserId",
                table: "RecruitmentCompanies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecruitmentCompanies",
                table: "RecruitmentCompanies");

            migrationBuilder.RenameTable(
                name: "RecruitmentCompanies",
                newName: "RecruitmentCompanys");

            migrationBuilder.RenameIndex(
                name: "IX_RecruitmentCompanies_UserId",
                table: "RecruitmentCompanys",
                newName: "IX_RecruitmentCompanys_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecruitmentCompanys",
                table: "RecruitmentCompanys",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_RecruitmentCompanys_CompanyId",
                table: "Jobs",
                column: "CompanyId",
                principalTable: "RecruitmentCompanys",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecruitmentCompanys_Users_UserId",
                table: "RecruitmentCompanys",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

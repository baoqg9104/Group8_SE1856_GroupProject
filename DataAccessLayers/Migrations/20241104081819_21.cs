using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayers.Migrations
{
    /// <inheritdoc />
    public partial class _21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberSkill_Members_MemberId",
                table: "MemberSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberSkill_Skills_SkillId",
                table: "MemberSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberSkill",
                table: "MemberSkill");

            migrationBuilder.RenameTable(
                name: "MemberSkill",
                newName: "MemberSkills");

            migrationBuilder.RenameIndex(
                name: "IX_MemberSkill_SkillId",
                table: "MemberSkills",
                newName: "IX_MemberSkills_SkillId");

            migrationBuilder.RenameIndex(
                name: "IX_MemberSkill_MemberId",
                table: "MemberSkills",
                newName: "IX_MemberSkills_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberSkills",
                table: "MemberSkills",
                column: "MemberSkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSkills_Members_MemberId",
                table: "MemberSkills",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSkills_Skills_SkillId",
                table: "MemberSkills",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "SkillId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberSkills_Members_MemberId",
                table: "MemberSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberSkills_Skills_SkillId",
                table: "MemberSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberSkills",
                table: "MemberSkills");

            migrationBuilder.RenameTable(
                name: "MemberSkills",
                newName: "MemberSkill");

            migrationBuilder.RenameIndex(
                name: "IX_MemberSkills_SkillId",
                table: "MemberSkill",
                newName: "IX_MemberSkill_SkillId");

            migrationBuilder.RenameIndex(
                name: "IX_MemberSkills_MemberId",
                table: "MemberSkill",
                newName: "IX_MemberSkill_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberSkill",
                table: "MemberSkill",
                column: "MemberSkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSkill_Members_MemberId",
                table: "MemberSkill",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSkill_Skills_SkillId",
                table: "MemberSkill",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "SkillId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

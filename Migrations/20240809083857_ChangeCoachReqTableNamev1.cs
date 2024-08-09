using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballMgm.Api.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCoachReqTableNamev1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoachRequest_Users_UserId",
                table: "CoachRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoachRequest",
                table: "CoachRequest");

            migrationBuilder.RenameTable(
                name: "CoachRequest",
                newName: "CoachRequests");

            migrationBuilder.RenameIndex(
                name: "IX_CoachRequest_UserId",
                table: "CoachRequests",
                newName: "IX_CoachRequests_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoachRequests",
                table: "CoachRequests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CoachRequests_Users_UserId",
                table: "CoachRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoachRequests_Users_UserId",
                table: "CoachRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoachRequests",
                table: "CoachRequests");

            migrationBuilder.RenameTable(
                name: "CoachRequests",
                newName: "CoachRequest");

            migrationBuilder.RenameIndex(
                name: "IX_CoachRequests_UserId",
                table: "CoachRequest",
                newName: "IX_CoachRequest_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoachRequest",
                table: "CoachRequest",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CoachRequest_Users_UserId",
                table: "CoachRequest",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectClock.Database.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationUser_Organizations_OrganizationId",
                table: "OrganizationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationUser_Users_UserId",
                table: "OrganizationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProject_Projects_ProjectId",
                table: "UserProject");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProject_Users_UserId",
                table: "UserProject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProject",
                table: "UserProject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganizationUser",
                table: "OrganizationUser");

            migrationBuilder.RenameTable(
                name: "UserProject",
                newName: "UserProjects");

            migrationBuilder.RenameTable(
                name: "OrganizationUser",
                newName: "OrganizationsUsers");

            migrationBuilder.RenameIndex(
                name: "IX_UserProject_UserId",
                table: "UserProjects",
                newName: "IX_UserProjects_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserProject_ProjectId",
                table: "UserProjects",
                newName: "IX_UserProjects_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganizationUser_OrganizationId",
                table: "OrganizationsUsers",
                newName: "IX_OrganizationsUsers_OrganizationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProjects",
                table: "UserProjects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganizationsUsers",
                table: "OrganizationsUsers",
                columns: new[] { "UserId", "OrganizationId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationsUsers_Organizations_OrganizationId",
                table: "OrganizationsUsers",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationsUsers_Users_UserId",
                table: "OrganizationsUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProjects_Projects_ProjectId",
                table: "UserProjects",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProjects_Users_UserId",
                table: "UserProjects",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationsUsers_Organizations_OrganizationId",
                table: "OrganizationsUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationsUsers_Users_UserId",
                table: "OrganizationsUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProjects_Projects_ProjectId",
                table: "UserProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProjects_Users_UserId",
                table: "UserProjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProjects",
                table: "UserProjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganizationsUsers",
                table: "OrganizationsUsers");

            migrationBuilder.RenameTable(
                name: "UserProjects",
                newName: "UserProject");

            migrationBuilder.RenameTable(
                name: "OrganizationsUsers",
                newName: "OrganizationUser");

            migrationBuilder.RenameIndex(
                name: "IX_UserProjects_UserId",
                table: "UserProject",
                newName: "IX_UserProject_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserProjects_ProjectId",
                table: "UserProject",
                newName: "IX_UserProject_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_OrganizationsUsers_OrganizationId",
                table: "OrganizationUser",
                newName: "IX_OrganizationUser_OrganizationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProject",
                table: "UserProject",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganizationUser",
                table: "OrganizationUser",
                columns: new[] { "UserId", "OrganizationId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationUser_Organizations_OrganizationId",
                table: "OrganizationUser",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationUser_Users_UserId",
                table: "OrganizationUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProject_Projects_ProjectId",
                table: "UserProject",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProject_Users_UserId",
                table: "UserProject",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectClock.Database.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartWork",
                table: "WorkingTimes",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "EndWork",
                table: "WorkingTimes",
                newName: "EndTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "WorkingTimes",
                newName: "StartWork");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "WorkingTimes",
                newName: "EndWork");
        }
    }
}

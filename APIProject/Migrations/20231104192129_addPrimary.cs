using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIProject.Migrations
{
    /// <inheritdoc />
    public partial class addPrimary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "eventID",
                table: "googleCalendars",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_googleCalendars",
                table: "googleCalendars",
                column: "eventID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_googleCalendars",
                table: "googleCalendars");

            migrationBuilder.AlterColumn<string>(
                name: "eventID",
                table: "googleCalendars",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}

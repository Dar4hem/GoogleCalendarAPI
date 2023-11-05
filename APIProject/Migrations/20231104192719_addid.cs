using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIProject.Migrations
{
    /// <inheritdoc />
    public partial class addid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_googleCalendars",
                table: "googleCalendars");

            migrationBuilder.AlterColumn<string>(
                name: "eventID",
                table: "googleCalendars",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "googleCalendars",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_googleCalendars",
                table: "googleCalendars",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_googleCalendars",
                table: "googleCalendars");

            migrationBuilder.DropColumn(
                name: "id",
                table: "googleCalendars");

            migrationBuilder.AlterColumn<string>(
                name: "eventID",
                table: "googleCalendars",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_googleCalendars",
                table: "googleCalendars",
                column: "eventID");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIProject.Migrations
{
    /// <inheritdoc />
    public partial class attcURL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Attachment",
                table: "googleCalendars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attachment",
                table: "googleCalendars");
        }
    }
}

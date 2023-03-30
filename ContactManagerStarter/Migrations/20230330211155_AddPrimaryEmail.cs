using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactManagerStarter.Migrations
{
    /// <inheritdoc />
    public partial class AddPrimaryEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int?>(
                name: "PrimaryEmailId",
                table: "Contacts",
                type: "int",
                nullable: true);
            migrationBuilder.DropColumn(
                name: "PrimaryEmail",
                table: "Contact");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrimaryEmailId",
                table: "Contacts");
        }
    }
}

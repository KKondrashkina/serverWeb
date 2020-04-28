using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopMigration.DataAccess.Migrations
{
    public partial class AddBirthDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BirthDay",
                table: "Customers",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDay",
                table: "Customers");
        }
    }
}

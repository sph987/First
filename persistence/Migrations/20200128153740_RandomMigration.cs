using Microsoft.EntityFrameworkCore.Migrations;

namespace persistence.Migrations
{
    public partial class RandomMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RandomString",
                table: "Activities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RandomString",
                table: "Activities");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Asp.DataAccess.Migrations
{
    public partial class UseCaseLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UseCaseLogs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UseCaseLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

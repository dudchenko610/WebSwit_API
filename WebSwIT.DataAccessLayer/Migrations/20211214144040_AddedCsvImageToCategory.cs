using Microsoft.EntityFrameworkCore.Migrations;

namespace WebSwIT.DataAccessLayer.Migrations
{
    public partial class AddedCsvImageToCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SvgImage",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SvgImage",
                table: "Categories");
        }
    }
}

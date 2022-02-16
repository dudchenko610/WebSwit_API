using Microsoft.EntityFrameworkCore.Migrations;

namespace WebSwIT.DataAccessLayer.Migrations
{
    public partial class AddFieldLinkToWorkSample : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "WorkSamples",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "WorkSamples");
        }
    }
}

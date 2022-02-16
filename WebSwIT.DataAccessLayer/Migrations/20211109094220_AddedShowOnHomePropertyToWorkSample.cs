using Microsoft.EntityFrameworkCore.Migrations;

namespace WebSwIT.DataAccessLayer.Migrations
{
    public partial class AddedShowOnHomePropertyToWorkSample : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowOnHome",
                table: "WorkSamples",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowOnHome",
                table: "WorkSamples");
        }
    }
}

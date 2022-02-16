using Microsoft.EntityFrameworkCore.Migrations;

namespace WebSwIT.DataAccessLayer.Migrations
{
    public partial class ChangedErrorFKOnWorkSamplePicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkSamplePictures_Technologies_WorkSampleId",
                table: "WorkSamplePictures");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSamplePictures_WorkSamples_WorkSampleId",
                table: "WorkSamplePictures",
                column: "WorkSampleId",
                principalTable: "WorkSamples",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkSamplePictures_WorkSamples_WorkSampleId",
                table: "WorkSamplePictures");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSamplePictures_Technologies_WorkSampleId",
                table: "WorkSamplePictures",
                column: "WorkSampleId",
                principalTable: "Technologies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

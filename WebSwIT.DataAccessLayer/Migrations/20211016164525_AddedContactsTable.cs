using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebSwIT.DataAccessLayer.Migrations
{
    public partial class AddedContactsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId2 = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contacts_AspNetUsers_UserId2",
                        column: x => x.UserId2,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_UserId1",
                table: "Contacts",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_UserId2",
                table: "Contacts",
                column: "UserId2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}

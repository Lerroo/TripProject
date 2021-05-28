using Microsoft.EntityFrameworkCore.Migrations;

namespace FastTripApp.Web.Migrations
{
    public partial class a : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_Id",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Reviews",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_Id",
                table: "Reviews",
                newName: "IX_Reviews_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_UserId",
                table: "Reviews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_UserId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Reviews",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                newName: "IX_Reviews_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_Id",
                table: "Reviews",
                column: "Id",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

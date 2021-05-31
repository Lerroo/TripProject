using Microsoft.EntityFrameworkCore.Migrations;

namespace FastTripApp.Web.Migrations
{
    public partial class firstdsadadadad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Reviews_ReviewId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_ReviewId",
                table: "Trips");

            migrationBuilder.AddColumn<int>(
                name: "ReviewId1",
                table: "Trips",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TripId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_ReviewId1",
                table: "Trips",
                column: "ReviewId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Reviews_ReviewId1",
                table: "Trips",
                column: "ReviewId1",
                principalTable: "Reviews",
                principalColumn: "ReviewId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Reviews_ReviewId1",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_ReviewId1",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "ReviewId1",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "TripId",
                table: "Reviews");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_ReviewId",
                table: "Trips",
                column: "ReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Reviews_ReviewId",
                table: "Trips",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "ReviewId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

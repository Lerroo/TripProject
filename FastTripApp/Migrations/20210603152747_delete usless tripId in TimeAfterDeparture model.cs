using Microsoft.EntityFrameworkCore.Migrations;

namespace FastTripApp.Web.Migrations
{
    public partial class deleteuslesstripIdinTimeAfterDeparturemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeAfterDepartures_Trips_TripId",
                table: "TimeAfterDepartures");

            migrationBuilder.DropIndex(
                name: "IX_TimeAfterDepartures_TripId",
                table: "TimeAfterDepartures");

            migrationBuilder.DropColumn(
                name: "TripId",
                table: "TimeAfterDepartures");

            migrationBuilder.AddColumn<int>(
                name: "TimeAfterDepartureId",
                table: "Trips",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_TimeAfterDepartureId",
                table: "Trips",
                column: "TimeAfterDepartureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_TimeAfterDepartures_TimeAfterDepartureId",
                table: "Trips",
                column: "TimeAfterDepartureId",
                principalTable: "TimeAfterDepartures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_TimeAfterDepartures_TimeAfterDepartureId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_TimeAfterDepartureId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "TimeAfterDepartureId",
                table: "Trips");

            migrationBuilder.AddColumn<int>(
                name: "TripId",
                table: "TimeAfterDepartures",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeAfterDepartures_TripId",
                table: "TimeAfterDepartures",
                column: "TripId",
                unique: true,
                filter: "[TripId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeAfterDepartures_Trips_TripId",
                table: "TimeAfterDepartures",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

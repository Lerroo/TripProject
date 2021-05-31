using Microsoft.EntityFrameworkCore.Migrations;

namespace FastTripApp.Web.Migrations
{
    public partial class firstdsadada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryTrips_TimeAfterDeparture_TimeAfterDepartureId",
                table: "HistoryTrips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_TimeAfterDeparture_TimeAfterDepartureId",
                table: "Trips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeAfterDeparture",
                table: "TimeAfterDeparture");

            migrationBuilder.RenameTable(
                name: "TimeAfterDeparture",
                newName: "TimeAfterDepartures");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeAfterDepartures",
                table: "TimeAfterDepartures",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryTrips_TimeAfterDepartures_TimeAfterDepartureId",
                table: "HistoryTrips",
                column: "TimeAfterDepartureId",
                principalTable: "TimeAfterDepartures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_HistoryTrips_TimeAfterDepartures_TimeAfterDepartureId",
                table: "HistoryTrips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_TimeAfterDepartures_TimeAfterDepartureId",
                table: "Trips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeAfterDepartures",
                table: "TimeAfterDepartures");

            migrationBuilder.RenameTable(
                name: "TimeAfterDepartures",
                newName: "TimeAfterDeparture");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeAfterDeparture",
                table: "TimeAfterDeparture",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryTrips_TimeAfterDeparture_TimeAfterDepartureId",
                table: "HistoryTrips",
                column: "TimeAfterDepartureId",
                principalTable: "TimeAfterDeparture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_TimeAfterDeparture_TimeAfterDepartureId",
                table: "Trips",
                column: "TimeAfterDepartureId",
                principalTable: "TimeAfterDeparture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

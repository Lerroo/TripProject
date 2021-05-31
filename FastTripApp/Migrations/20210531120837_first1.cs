using Microsoft.EntityFrameworkCore.Migrations;

namespace FastTripApp.Web.Migrations
{
    public partial class first1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeAfterDepartures_Trips_TripId",
                table: "TimeAfterDepartures");

            migrationBuilder.DropIndex(
                name: "IX_TimeAfterDepartures_TripId",
                table: "TimeAfterDepartures");

            migrationBuilder.AlterColumn<int>(
                name: "TripId",
                table: "TimeAfterDepartures",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeAfterDepartures_Trips_TripId",
                table: "TimeAfterDepartures");

            migrationBuilder.DropIndex(
                name: "IX_TimeAfterDepartures_TripId",
                table: "TimeAfterDepartures");

            migrationBuilder.AlterColumn<int>(
                name: "TripId",
                table: "TimeAfterDepartures",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeAfterDepartures_TripId",
                table: "TimeAfterDepartures",
                column: "TripId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeAfterDepartures_Trips_TripId",
                table: "TimeAfterDepartures",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

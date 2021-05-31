using Microsoft.EntityFrameworkCore.Migrations;

namespace FastTripApp.Web.Migrations
{
    public partial class firstendstatusdaddada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Trips",
                newName: "StatusEnum");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "HistoryTrips",
                newName: "StatusEnum");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StatusEnum",
                table: "Trips",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "StatusEnum",
                table: "HistoryTrips",
                newName: "Status");
        }
    }
}

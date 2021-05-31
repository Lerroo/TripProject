using Microsoft.EntityFrameworkCore.Migrations;

namespace FastTripApp.Web.Migrations
{
    public partial class firstendstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "HistoryTrips",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "HistoryTrips");
        }
    }
}

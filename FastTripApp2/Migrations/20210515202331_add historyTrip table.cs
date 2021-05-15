using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace banan.Migrations
{
    public partial class addhistoryTriptable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoryTrips",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TripId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    TimePlain = table.Column<DateTime>(nullable: false),
                    EstimatedTime = table.Column<TimeSpan>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Descriprion = table.Column<string>(nullable: false),
                    StartTrip = table.Column<DateTime>(nullable: true),
                    EndTrip = table.Column<DateTime>(nullable: true),
                    TimeTrack = table.Column<TimeSpan>(nullable: true),
                    AddressStart = table.Column<string>(nullable: false),
                    AddressEnd = table.Column<string>(nullable: false),
                    AddressStartLatitude = table.Column<string>(nullable: true),
                    AddressStartLongitude = table.Column<string>(nullable: true),
                    AddressEndLatitude = table.Column<string>(nullable: true),
                    AddressEndLongitude = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryTrips", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryTrips");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Tim14HCI.Migrations
{
    public partial class testDAta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "onWayStations",
                keyColumn: "OnWayStationID",
                keyValue: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "onWayStations",
                columns: new[] { "OnWayStationID", "Price", "StationID", "StationOrder", "Time", "TrainLineID", "isEndStation" },
                values: new object[] { 2, 10f, 2, 2, 10f, 1, true });
        }
    }
}

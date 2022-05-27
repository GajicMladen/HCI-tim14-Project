using Microsoft.EntityFrameworkCore.Migrations;

namespace Tim14HCI.Migrations
{
    public partial class Positions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "stations",
                keyColumn: "StationID",
                keyValue: 3,
                column: "position_x",
                value: 40);

            migrationBuilder.UpdateData(
                table: "stations",
                keyColumn: "StationID",
                keyValue: 4,
                column: "position_y",
                value: 60);

            migrationBuilder.UpdateData(
                table: "stations",
                keyColumn: "StationID",
                keyValue: 6,
                column: "position_x",
                value: 160);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "stations",
                keyColumn: "StationID",
                keyValue: 3,
                column: "position_x",
                value: 75);

            migrationBuilder.UpdateData(
                table: "stations",
                keyColumn: "StationID",
                keyValue: 4,
                column: "position_y",
                value: 50);

            migrationBuilder.UpdateData(
                table: "stations",
                keyColumn: "StationID",
                keyValue: 6,
                column: "position_x",
                value: 130);
        }
    }
}

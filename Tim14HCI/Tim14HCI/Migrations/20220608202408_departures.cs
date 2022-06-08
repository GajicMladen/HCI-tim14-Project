using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tim14HCI.Migrations
{
    public partial class departures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "departures",
                columns: new[] { "DepartureID", "StartTime", "TrainLineID" },
                values: new object[] { 1, new DateTime(2022, 6, 8, 12, 30, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.InsertData(
                table: "departures",
                columns: new[] { "DepartureID", "StartTime", "TrainLineID" },
                values: new object[] { 2, new DateTime(2022, 6, 8, 15, 40, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.InsertData(
                table: "departures",
                columns: new[] { "DepartureID", "StartTime", "TrainLineID" },
                values: new object[] { 3, new DateTime(2022, 6, 8, 20, 15, 0, 0, DateTimeKind.Unspecified), 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "departures",
                keyColumn: "DepartureID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "departures",
                keyColumn: "DepartureID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "departures",
                keyColumn: "DepartureID",
                keyValue: 3);
        }
    }
}

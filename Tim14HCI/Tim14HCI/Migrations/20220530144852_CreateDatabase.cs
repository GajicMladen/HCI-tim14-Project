using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tim14HCI.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "stations",
                columns: table => new
                {
                    StationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    position_x = table.Column<int>(nullable: false),
                    position_y = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stations", x => x.StationID);
                });

            migrationBuilder.CreateTable(
                name: "trains",
                columns: table => new
                {
                    TrainID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    MaxSpeed = table.Column<float>(nullable: false),
                    Capacity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trains", x => x.TrainID);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    UserRole = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "linkedStations",
                columns: table => new
                {
                    Station1ID = table.Column<int>(nullable: false),
                    Station2ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_linkedStations", x => new { x.Station1ID, x.Station2ID });
                    table.ForeignKey(
                        name: "FK_linkedStations_stations_Station1ID",
                        column: x => x.Station1ID,
                        principalTable: "stations",
                        principalColumn: "StationID");
                    table.ForeignKey(
                        name: "FK_linkedStations_stations_Station2ID",
                        column: x => x.Station2ID,
                        principalTable: "stations",
                        principalColumn: "StationID");
                });

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    TicketID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ForReservation = table.Column<bool>(nullable: false),
                    DepartureID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tickets", x => x.TicketID);
                    table.ForeignKey(
                        name: "FK_tickets_users_UserID",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "trainLines",
                columns: table => new
                {
                    TrainLineID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartStationID = table.Column<int>(nullable: false),
                    EndStationID = table.Column<int>(nullable: false),
                    EndStationOnWayStationID = table.Column<int>(nullable: true),
                    TrainID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trainLines", x => x.TrainLineID);
                    table.ForeignKey(
                        name: "FK_trainLines_stations_StartStationID",
                        column: x => x.StartStationID,
                        principalTable: "stations",
                        principalColumn: "StationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_trainLines_trains_TrainID",
                        column: x => x.TrainID,
                        principalTable: "trains",
                        principalColumn: "TrainID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "departures",
                columns: table => new
                {
                    DepartureID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    startDate = table.Column<DateTime>(nullable: false),
                    endDate = table.Column<DateTime>(nullable: false),
                    StartTimeEveryday = table.Column<DateTime>(nullable: false),
                    TrainLineID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departures", x => x.DepartureID);
                    table.ForeignKey(
                        name: "FK_departures_trainLines_TrainLineID",
                        column: x => x.TrainLineID,
                        principalTable: "trainLines",
                        principalColumn: "TrainLineID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "onWayStations",
                columns: table => new
                {
                    OnWayStationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StationOrder = table.Column<int>(nullable: false),
                    Price = table.Column<float>(nullable: false),
                    Time = table.Column<float>(nullable: false),
                    StationID = table.Column<int>(nullable: false),
                    TrainLineID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_onWayStations", x => x.OnWayStationID);
                    table.ForeignKey(
                        name: "FK_onWayStations_stations_StationID",
                        column: x => x.StationID,
                        principalTable: "stations",
                        principalColumn: "StationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_onWayStations_trainLines_TrainLineID",
                        column: x => x.TrainLineID,
                        principalTable: "trainLines",
                        principalColumn: "TrainLineID");
                });

            migrationBuilder.InsertData(
                table: "stations",
                columns: new[] { "StationID", "Name", "position_x", "position_y" },
                values: new object[,]
                {
                    { 1, "Beograd", 85, 95 },
                    { 2, "Novi Sad", 30, 50 },
                    { 3, "Subotica", 40, 10 },
                    { 4, "Zrenjanin", 70, 60 },
                    { 5, "Niš", 130, 160 },
                    { 6, "Leskovac", 160, 230 }
                });

            migrationBuilder.InsertData(
                table: "trains",
                columns: new[] { "TrainID", "Capacity", "MaxSpeed", "Name" },
                values: new object[,]
                {
                    { 1, 120, 160f, "Soko" },
                    { 2, 50, 60f, "Cira" },
                    { 3, 100, 100f, "Krsko" },
                    { 4, 200, 200f, "Soko II" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserID", "Email", "FirstName", "LastName", "Password", "Phone", "UserRole" },
                values: new object[,]
                {
                    { 1, "mg00@gmail.com", "Mladen", "Gajic", "mladen123", "05603051", 1 },
                    { 2, "ns00@gmail.com", "Nikola", "Stepic", "aaa", "123432", 1 },
                    { 3, "jj00@gmail.com", "Jovan", "Jovanovic", "bbb", "214t5654", 0 },
                    { 4, "dt00@gmail.com", "Djordje", "Tomic", "ccc", "+381064428108", 0 }
                });

            migrationBuilder.InsertData(
                table: "linkedStations",
                columns: new[] { "Station1ID", "Station2ID" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 3 },
                    { 1, 5 },
                    { 5, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_departures_TrainLineID",
                table: "departures",
                column: "TrainLineID");

            migrationBuilder.CreateIndex(
                name: "IX_linkedStations_Station2ID",
                table: "linkedStations",
                column: "Station2ID");

            migrationBuilder.CreateIndex(
                name: "IX_onWayStations_StationID",
                table: "onWayStations",
                column: "StationID");

            migrationBuilder.CreateIndex(
                name: "IX_onWayStations_TrainLineID",
                table: "onWayStations",
                column: "TrainLineID");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_DepartureID",
                table: "tickets",
                column: "DepartureID");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_UserID",
                table: "tickets",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_trainLines_EndStationOnWayStationID",
                table: "trainLines",
                column: "EndStationOnWayStationID");

            migrationBuilder.CreateIndex(
                name: "IX_trainLines_StartStationID",
                table: "trainLines",
                column: "StartStationID");

            migrationBuilder.CreateIndex(
                name: "IX_trainLines_TrainID",
                table: "trainLines",
                column: "TrainID");

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_departures_DepartureID",
                table: "tickets",
                column: "DepartureID",
                principalTable: "departures",
                principalColumn: "DepartureID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_trainLines_onWayStations_EndStationOnWayStationID",
                table: "trainLines",
                column: "EndStationOnWayStationID",
                principalTable: "onWayStations",
                principalColumn: "OnWayStationID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_onWayStations_trainLines_TrainLineID",
                table: "onWayStations");

            migrationBuilder.DropTable(
                name: "linkedStations");

            migrationBuilder.DropTable(
                name: "tickets");

            migrationBuilder.DropTable(
                name: "departures");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "trainLines");

            migrationBuilder.DropTable(
                name: "onWayStations");

            migrationBuilder.DropTable(
                name: "trains");

            migrationBuilder.DropTable(
                name: "stations");
        }
    }
}

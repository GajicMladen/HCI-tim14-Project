using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tim14HCI.Migrations
{
    public partial class addedTickets : Migration
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
                    StartStationID = table.Column<int>(nullable: false),
                    EndStationID = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Seat = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tickets", x => x.TicketID);
                    table.ForeignKey(
                        name: "FK_tickets_stations_EndStationID",
                        column: x => x.EndStationID,
                        principalTable: "stations",
                        principalColumn: "StationID");
                    table.ForeignKey(
                        name: "FK_tickets_stations_StartStationID",
                        column: x => x.StartStationID,
                        principalTable: "stations",
                        principalColumn: "StationID");
                    table.ForeignKey(
                        name: "FK_tickets_users_UserID",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "UserID");
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
                    StartTime = table.Column<DateTime>(nullable: false),
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
                    isEndStation = table.Column<bool>(nullable: false),
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
                    { 4, 2 },
                    { 1, 5 },
                    { 5, 6 }
                });

            migrationBuilder.InsertData(
                table: "trainLines",
                columns: new[] { "TrainLineID", "EndStationID", "EndStationOnWayStationID", "StartStationID", "TrainID" },
                values: new object[,]
                {
                    { 1, 2, null, 1, 1 },
                    { 2, 6, null, 3, 4 }
                });

            migrationBuilder.InsertData(
                table: "departures",
                columns: new[] { "DepartureID", "StartTime", "TrainLineID" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 1, 8, 12, 30, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, new DateTime(2022, 2, 8, 15, 40, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, new DateTime(2022, 3, 8, 20, 15, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4, new DateTime(2022, 4, 14, 12, 30, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 5, new DateTime(2022, 5, 14, 15, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 6, new DateTime(2022, 6, 14, 18, 40, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 7, new DateTime(2022, 7, 14, 21, 15, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 8, new DateTime(2022, 8, 15, 15, 40, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 9, new DateTime(2022, 9, 15, 18, 30, 0, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.InsertData(
                table: "onWayStations",
                columns: new[] { "OnWayStationID", "Price", "StationID", "StationOrder", "Time", "TrainLineID", "isEndStation" },
                values: new object[,]
                {
                    { 1, 800f, 2, 1, 30f, 1, true },
                    { 2, 400f, 2, 1, 35f, 2, false },
                    { 3, 550f, 1, 2, 35f, 2, false },
                    { 4, 700f, 5, 3, 50f, 2, false },
                    { 5, 400f, 6, 4, 30f, 2, true }
                });

            migrationBuilder.InsertData(
                table: "tickets",
                columns: new[] { "TicketID", "DepartureID", "EndStationID", "ForReservation", "Price", "Seat", "StartStationID", "UserID" },
                values: new object[,]
                {
                    { 1, 1, 2, false, 800.0, 2, 1, 4 },
                    { 13, 1, 2, true, 400.0, 9, 3, 3 },
                    { 15, 1, 5, true, 1650.0, 10, 3, 4 },
                    { 2, 2, 2, true, 400.0, 1, 3, 3 },
                    { 3, 2, 2, true, 400.0, 2, 3, 3 },
                    { 4, 2, 2, true, 400.0, 3, 3, 3 },
                    { 12, 2, 1, false, 950.0, 12, 3, 3 },
                    { 5, 3, 1, false, 950.0, 2, 3, 4 },
                    { 14, 3, 1, false, 950.0, 4, 3, 4 },
                    { 6, 4, 5, true, 1650.0, 3, 3, 3 },
                    { 7, 5, 6, false, 2050.0, 4, 3, 4 },
                    { 8, 6, 2, false, 400.0, 5, 3, 3 },
                    { 9, 7, 5, true, 1650.0, 6, 3, 4 },
                    { 10, 8, 1, false, 950.0, 7, 3, 3 },
                    { 11, 9, 6, false, 2050.0, 8, 3, 4 }
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
                name: "IX_tickets_EndStationID",
                table: "tickets",
                column: "EndStationID");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_StartStationID",
                table: "tickets",
                column: "StartStationID");

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
                principalColumn: "DepartureID");

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

using Microsoft.EntityFrameworkCore.Migrations;

namespace Tim14HCI.Migrations
{
    public partial class OnWayStationChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_onWayStations_trainLines_TrainLineID1",
                table: "onWayStations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_onWayStations",
                table: "onWayStations");

            migrationBuilder.DropIndex(
                name: "IX_onWayStations_TrainLineID1",
                table: "onWayStations");

            migrationBuilder.DropColumn(
                name: "TrainLineID1",
                table: "onWayStations");

            migrationBuilder.AddColumn<int>(
                name: "EndStationOnWayStationID",
                table: "trainLines",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OnWayStationID",
                table: "onWayStations",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_onWayStations",
                table: "onWayStations",
                column: "OnWayStationID");

            migrationBuilder.CreateIndex(
                name: "IX_trainLines_EndStationOnWayStationID",
                table: "trainLines",
                column: "EndStationOnWayStationID");

            migrationBuilder.CreateIndex(
                name: "IX_onWayStations_TrainLineID",
                table: "onWayStations",
                column: "TrainLineID");

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
                name: "FK_trainLines_onWayStations_EndStationOnWayStationID",
                table: "trainLines");

            migrationBuilder.DropIndex(
                name: "IX_trainLines_EndStationOnWayStationID",
                table: "trainLines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_onWayStations",
                table: "onWayStations");

            migrationBuilder.DropIndex(
                name: "IX_onWayStations_TrainLineID",
                table: "onWayStations");

            migrationBuilder.DropColumn(
                name: "EndStationOnWayStationID",
                table: "trainLines");

            migrationBuilder.DropColumn(
                name: "OnWayStationID",
                table: "onWayStations");

            migrationBuilder.AddColumn<int>(
                name: "TrainLineID1",
                table: "onWayStations",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_onWayStations",
                table: "onWayStations",
                columns: new[] { "TrainLineID", "StationOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_onWayStations_TrainLineID1",
                table: "onWayStations",
                column: "TrainLineID1",
                unique: true,
                filter: "[TrainLineID1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_onWayStations_trainLines_TrainLineID1",
                table: "onWayStations",
                column: "TrainLineID1",
                principalTable: "trainLines",
                principalColumn: "TrainLineID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

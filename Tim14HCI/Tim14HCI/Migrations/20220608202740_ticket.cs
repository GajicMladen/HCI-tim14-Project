using Microsoft.EntityFrameworkCore.Migrations;

namespace Tim14HCI.Migrations
{
    public partial class ticket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "tickets",
                columns: new[] { "TicketID", "DepartureID", "ForReservation", "UserID" },
                values: new object[] { 1, 1, false, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tickets",
                keyColumn: "TicketID",
                keyValue: 1);
        }
    }
}

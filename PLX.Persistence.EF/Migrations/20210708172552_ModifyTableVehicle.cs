using Microsoft.EntityFrameworkCore.Migrations;

namespace PLX.Persistence.EF.Migrations
{
    public partial class ModifyTableVehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vehicle_LicensePlate",
                table: "Vehicle");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_LicensePlate",
                table: "Vehicle",
                column: "LicensePlate",
                unique: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace PLX.Persistence.EF.Migrations
{
    public partial class RenameCreateTimeOfOTP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateTime11",
                table: "OTP",
                newName: "CreateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateTime",
                table: "OTP",
                newName: "CreateTime11");
        }
    }
}

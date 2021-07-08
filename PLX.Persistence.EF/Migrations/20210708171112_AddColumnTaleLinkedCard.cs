using Microsoft.EntityFrameworkCore.Migrations;

namespace PLX.Persistence.EF.Migrations
{
    public partial class AddColumnTaleLinkedCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "LinkedCard",
                type: "boolean",
                nullable: true,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "LinkedCard");
        }
    }
}

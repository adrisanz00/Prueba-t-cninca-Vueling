using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    public partial class finalmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Transacciones",
                table: "Transacciones");

            migrationBuilder.RenameTable(
                name: "Transacciones",
                newName: "Transactions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "Transacciones");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transacciones",
                table: "Transacciones",
                column: "Id");
        }
    }
}

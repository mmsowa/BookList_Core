using Microsoft.EntityFrameworkCore.Migrations;

namespace BookListMVC.Migrations.AuthDb
{
    public partial class try_to_fix_ef_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AppUserBook",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "AppUserBook");
        }
    }
}

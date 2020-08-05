using Microsoft.EntityFrameworkCore.Migrations;

namespace BookListMVC.Migrations.AuthDb
{
    public partial class try_to_fix_ef_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserBook");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUserBook",
                columns: table => new
                {
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    AppUserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserBook", x => new { x.AppUserId, x.BookId });
                    table.ForeignKey(
                        name: "FK_AppUserBook_AspNetUsers_AppUserId1",
                        column: x => x.AppUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppUserBook_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserBook_AppUserId1",
                table: "AppUserBook",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserBook_BookId",
                table: "AppUserBook",
                column: "BookId");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace BookListMVC.Migrations.AuthDb {
  public partial class Configure_AppUser_Book_Many_To_Many : Migration {
    protected override void Up(MigrationBuilder migrationBuilder) {
      migrationBuilder.DropTable(
               name: "Books");

      migrationBuilder.DropForeignKey(
          name: "FK_Book_AspNetUsers_AppUserId",
          table: "Book");

      migrationBuilder.DropPrimaryKey(
          name: "PK_Book",
          table: "Book");

      migrationBuilder.DropIndex(
          name: "IX_Book_AppUserId",
          table: "Book");

      migrationBuilder.DropColumn(
          name: "AppUserId",
          table: "Book");

      migrationBuilder.RenameTable(
          name: "Book",
          newName: "Books");

      migrationBuilder.AddPrimaryKey(
          name: "PK_Books",
          table: "Books",
          column: "Id");

      migrationBuilder.CreateTable(
          name: "AppUserBook",
          columns: table => new {
            AppUserId = table.Column<int>(nullable: false),
            BookId = table.Column<int>(nullable: false),
            AppUserId1 = table.Column<string>(nullable: true)
          },
          constraints: table => {
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

    protected override void Down(MigrationBuilder migrationBuilder) {
      migrationBuilder.DropTable(
          name: "AppUserBook");

      migrationBuilder.DropPrimaryKey(
          name: "PK_Books",
          table: "Books");

      migrationBuilder.RenameTable(
          name: "Books",
          newName: "Book");

      migrationBuilder.AddColumn<string>(
          name: "AppUserId",
          table: "Book",
          type: "nvarchar(450)",
          nullable: true);

      migrationBuilder.AddPrimaryKey(
          name: "PK_Book",
          table: "Book",
          column: "Id");

      migrationBuilder.CreateIndex(
          name: "IX_Book_AppUserId",
          table: "Book",
          column: "AppUserId");

      migrationBuilder.AddForeignKey(
          name: "FK_Book_AspNetUsers_AppUserId",
          table: "Book",
          column: "AppUserId",
          principalTable: "AspNetUsers",
          principalColumn: "Id",
          onDelete: ReferentialAction.Restrict);
    }
  }
}

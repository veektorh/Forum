using Microsoft.EntityFrameworkCore.Migrations;

namespace Forum.Web.Migrations
{
    public partial class addpostStates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommentStates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CommentId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Vote = table.Column<bool>(nullable: false),
                    Saved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentStates_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentStates_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostStates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PostId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Vote = table.Column<bool>(nullable: false),
                    Saved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostStates_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostStates_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentStates_CommentId",
                table: "CommentStates",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentStates_UserId",
                table: "CommentStates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostStates_PostId",
                table: "PostStates",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostStates_UserId",
                table: "PostStates",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentStates");

            migrationBuilder.DropTable(
                name: "PostStates");
        }
    }
}

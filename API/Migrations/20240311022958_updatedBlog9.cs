using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatedBlog9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogCommentBlogReply");

            migrationBuilder.AlterColumn<int>(
                name: "CommentId",
                table: "BlogReplys",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_BlogReplys_CommentId",
                table: "BlogReplys",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogReplys_BlogComments_CommentId",
                table: "BlogReplys",
                column: "CommentId",
                principalTable: "BlogComments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogReplys_BlogComments_CommentId",
                table: "BlogReplys");

            migrationBuilder.DropIndex(
                name: "IX_BlogReplys_CommentId",
                table: "BlogReplys");

            migrationBuilder.AlterColumn<int>(
                name: "CommentId",
                table: "BlogReplys",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "BlogCommentBlogReply",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false),
                    RepliesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCommentBlogReply", x => new { x.CommentId, x.RepliesId });
                    table.ForeignKey(
                        name: "FK_BlogCommentBlogReply_BlogComments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "BlogComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogCommentBlogReply_BlogReplys_RepliesId",
                        column: x => x.RepliesId,
                        principalTable: "BlogReplys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogCommentBlogReply_RepliesId",
                table: "BlogCommentBlogReply",
                column: "RepliesId");
        }
    }
}

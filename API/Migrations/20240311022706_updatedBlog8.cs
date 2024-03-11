using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatedBlog8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogReplys_BlogComments_CommentId",
                table: "BlogReplys");

            migrationBuilder.DropIndex(
                name: "IX_BlogReplys_CommentId",
                table: "BlogReplys");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogCommentBlogReply");

            migrationBuilder.CreateIndex(
                name: "IX_BlogReplys_CommentId",
                table: "BlogReplys",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogReplys_BlogComments_CommentId",
                table: "BlogReplys",
                column: "CommentId",
                principalTable: "BlogComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

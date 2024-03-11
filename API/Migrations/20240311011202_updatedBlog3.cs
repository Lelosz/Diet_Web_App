using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatedBlog3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogComment_BlogPost_BlogPostId",
                table: "BlogComment");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogComment_Users_UserId",
                table: "BlogComment");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPost_Users_UserId",
                table: "BlogPost");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogReply_BlogComment_CommentId",
                table: "BlogReply");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogReply",
                table: "BlogReply");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogPost",
                table: "BlogPost");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogComment",
                table: "BlogComment");

            migrationBuilder.RenameTable(
                name: "BlogReply",
                newName: "BlogReplys");

            migrationBuilder.RenameTable(
                name: "BlogPost",
                newName: "BlogPosts");

            migrationBuilder.RenameTable(
                name: "BlogComment",
                newName: "BlogComments");

            migrationBuilder.RenameIndex(
                name: "IX_BlogReply_CommentId",
                table: "BlogReplys",
                newName: "IX_BlogReplys_CommentId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPost_UserId",
                table: "BlogPosts",
                newName: "IX_BlogPosts_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogComment_UserId",
                table: "BlogComments",
                newName: "IX_BlogComments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogComment_BlogPostId",
                table: "BlogComments",
                newName: "IX_BlogComments_BlogPostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogReplys",
                table: "BlogReplys",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogPosts",
                table: "BlogPosts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogComments",
                table: "BlogComments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogComments_BlogPosts_BlogPostId",
                table: "BlogComments",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogComments_Users_UserId",
                table: "BlogComments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Users_UserId",
                table: "BlogPosts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogReplys_BlogComments_CommentId",
                table: "BlogReplys",
                column: "CommentId",
                principalTable: "BlogComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogComments_BlogPosts_BlogPostId",
                table: "BlogComments");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogComments_Users_UserId",
                table: "BlogComments");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Users_UserId",
                table: "BlogPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogReplys_BlogComments_CommentId",
                table: "BlogReplys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogReplys",
                table: "BlogReplys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogPosts",
                table: "BlogPosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogComments",
                table: "BlogComments");

            migrationBuilder.RenameTable(
                name: "BlogReplys",
                newName: "BlogReply");

            migrationBuilder.RenameTable(
                name: "BlogPosts",
                newName: "BlogPost");

            migrationBuilder.RenameTable(
                name: "BlogComments",
                newName: "BlogComment");

            migrationBuilder.RenameIndex(
                name: "IX_BlogReplys_CommentId",
                table: "BlogReply",
                newName: "IX_BlogReply_CommentId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPosts_UserId",
                table: "BlogPost",
                newName: "IX_BlogPost_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogComments_UserId",
                table: "BlogComment",
                newName: "IX_BlogComment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogComments_BlogPostId",
                table: "BlogComment",
                newName: "IX_BlogComment_BlogPostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogReply",
                table: "BlogReply",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogPost",
                table: "BlogPost",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogComment",
                table: "BlogComment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogComment_BlogPost_BlogPostId",
                table: "BlogComment",
                column: "BlogPostId",
                principalTable: "BlogPost",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogComment_Users_UserId",
                table: "BlogComment",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPost_Users_UserId",
                table: "BlogPost",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogReply_BlogComment_CommentId",
                table: "BlogReply",
                column: "CommentId",
                principalTable: "BlogComment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

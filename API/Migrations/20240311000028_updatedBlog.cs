using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatedBlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "BlogReply",
                newName: "ReplyContent");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "BlogPost",
                newName: "PostContent");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "BlogComment",
                newName: "CommentContent");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReplyContent",
                table: "BlogReply",
                newName: "Text");

            migrationBuilder.RenameColumn(
                name: "PostContent",
                table: "BlogPost",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "CommentContent",
                table: "BlogComment",
                newName: "Content");
        }
    }
}

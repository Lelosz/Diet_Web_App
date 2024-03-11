using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatedBlog2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogComment_BlogPost_PostId",
                table: "BlogComment");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "BlogComment");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "BlogComment",
                newName: "BlogPostId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogComment_PostId",
                table: "BlogComment",
                newName: "IX_BlogComment_BlogPostId");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "BlogReply",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogComment_BlogPost_BlogPostId",
                table: "BlogComment",
                column: "BlogPostId",
                principalTable: "BlogPost",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogComment_BlogPost_BlogPostId",
                table: "BlogComment");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "BlogReply");

            migrationBuilder.RenameColumn(
                name: "BlogPostId",
                table: "BlogComment",
                newName: "PostId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogComment_BlogPostId",
                table: "BlogComment",
                newName: "IX_BlogComment_PostId");

            migrationBuilder.AddColumn<int>(
                name: "BlogId",
                table: "BlogComment",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogComment_BlogPost_PostId",
                table: "BlogComment",
                column: "PostId",
                principalTable: "BlogPost",
                principalColumn: "Id");
        }
    }
}

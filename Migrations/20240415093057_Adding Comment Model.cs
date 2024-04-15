using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeBlog.Migrations
{
    /// <inheritdoc />
    public partial class AddingCommentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogPostComment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogPostComment_BlogPosts_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a892b811-80bd-4e68-b3b7-0df95a0de725",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "205e5825-0872-4e6f-808a-b6f10cc496c5", "AQAAAAIAAYagAAAAEH2ifYgiUpmPVnP0U1hTyJxwI/dJjLsL0FLlNGBt4tUQI8UnloWcijxO/qBC5BKPvA==", "dc7d9e29-5390-4ce0-89ae-3c892d06bf65" });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostComment_BlogPostId",
                table: "BlogPostComment",
                column: "BlogPostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPostComment");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a892b811-80bd-4e68-b3b7-0df95a0de725",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b9c5015d-dc0b-4193-a92f-b705be0ff1b5", "AQAAAAIAAYagAAAAELWdwWQe/mAVz01MUpJILukIbg/FYS5/pa2IcRUzV8sfFdoA0BGLKUlOvuyOk1Kexg==", "1395b4a0-0781-411e-b477-5470a1d57a28" });
        }
    }
}

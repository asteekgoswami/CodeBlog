using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeBlog.Migrations
{
    /// <inheritdoc />
    public partial class officeintial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a892b811-80bd-4e68-b3b7-0df95a0de725",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2af19054-7936-47a1-aeed-981c9266b559", "AQAAAAIAAYagAAAAEAOGIqIqNzv0AzRP7SyzeBowpAYWgI55u5v/Y4aCV1PPm1lwGqaO1iamNABUeoYVSA==", "ff0038b3-d27f-4435-8738-a125e5bfedeb" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a892b811-80bd-4e68-b3b7-0df95a0de725",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7391ae17-0198-414b-a93e-ca7192cd4372", "AQAAAAIAAYagAAAAEND+xYVuYnOabAxoDh6Km3INLOQ8Xbh2ref2FrZJmliejsrELXuhomv8YQjRGLSdww==", "ab23814f-ede4-41b3-a16e-967af190c24a" });
        }
    }
}

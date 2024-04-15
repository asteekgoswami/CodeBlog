using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeBlog.Migrations
{
    /// <inheritdoc />
    public partial class trylike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a892b811-80bd-4e68-b3b7-0df95a0de725",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7391ae17-0198-414b-a93e-ca7192cd4372", "AQAAAAIAAYagAAAAEND+xYVuYnOabAxoDh6Km3INLOQ8Xbh2ref2FrZJmliejsrELXuhomv8YQjRGLSdww==", "ab23814f-ede4-41b3-a16e-967af190c24a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a892b811-80bd-4e68-b3b7-0df95a0de725",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e9bb9201-a178-4820-bc6f-97d74dea88e2", "AQAAAAIAAYagAAAAEL17A0pibeIvkcTHLS9ITpO4o7sIfWC8j6GRrxOIHQUMCp/ZDabBcB9vi2O9zMKGQg==", "b6e93b1b-6a26-41fc-9010-cf27290ed582" });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeBlog.Migrations
{
    /// <inheritdoc />
    public partial class initialo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a892b811-80bd-4e68-b3b7-0df95a0de725",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e9bb9201-a178-4820-bc6f-97d74dea88e2", "AQAAAAIAAYagAAAAEL17A0pibeIvkcTHLS9ITpO4o7sIfWC8j6GRrxOIHQUMCp/ZDabBcB9vi2O9zMKGQg==", "b6e93b1b-6a26-41fc-9010-cf27290ed582" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a892b811-80bd-4e68-b3b7-0df95a0de725",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9a949bfe-70eb-476a-a4f6-5d712c46c315", "AQAAAAIAAYagAAAAEP3eSoxRp4Bp3wXiu5nzC1e2a5D/wnrZTuJheX6ql6w9WLzCkDSb4ZGyNw0O8pwayA==", "0661f01d-ec20-4db9-b1de-6d573c58f69b" });
        }
    }
}

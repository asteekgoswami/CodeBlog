using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeBlog.Migrations
{
    /// <inheritdoc />
    public partial class traineeinitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a892b811-80bd-4e68-b3b7-0df95a0de725",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b9c5015d-dc0b-4193-a92f-b705be0ff1b5", "AQAAAAIAAYagAAAAELWdwWQe/mAVz01MUpJILukIbg/FYS5/pa2IcRUzV8sfFdoA0BGLKUlOvuyOk1Kexg==", "1395b4a0-0781-411e-b477-5470a1d57a28" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a892b811-80bd-4e68-b3b7-0df95a0de725",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2af19054-7936-47a1-aeed-981c9266b559", "AQAAAAIAAYagAAAAEAOGIqIqNzv0AzRP7SyzeBowpAYWgI55u5v/Y4aCV1PPm1lwGqaO1iamNABUeoYVSA==", "ff0038b3-d27f-4435-8738-a125e5bfedeb" });
        }
    }
}

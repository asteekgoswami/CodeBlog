using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CodeBlog.Migrations
{
    /// <inheritdoc />
    public partial class initial_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "70e9f6d7-14fb-4221-9dc4-45fe2167cf0c", "70e9f6d7-14fb-4221-9dc4-45fe2167cf0c", "SuperAdmin", "SuperAdmin" },
                    { "96ee7040-dbcd-44d4-a18f-d8b58eb3206f", "96ee7040-dbcd-44d4-a18f-d8b58eb3206f", "Admin", "Admin" },
                    { "db2309f0-c714-498f-9c34-2684143f3929", "db2309f0-c714-498f-9c34-2684143f3929", "User", "User" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a892b811-80bd-4e68-b3b7-0df95a0de725", 0, "2c98d77c-b1a4-4ff2-80d2-5c61485f32cc", "superadmin@gmail.com", false, false, null, "SUPERADMIN@GMAIL.COM", "SUPERADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEC3ADcWLMrdGWoQ2xDnRpeaT9C+hE+mh9yFPnn8M/uCj5Bb+s9aqydUAOBk26HHOZw==", null, false, "0527f06b-d3d1-4cc2-88e4-fd83b8508614", false, "superadmin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "70e9f6d7-14fb-4221-9dc4-45fe2167cf0c", "a892b811-80bd-4e68-b3b7-0df95a0de725" },
                    { "96ee7040-dbcd-44d4-a18f-d8b58eb3206f", "a892b811-80bd-4e68-b3b7-0df95a0de725" },
                    { "db2309f0-c714-498f-9c34-2684143f3929", "a892b811-80bd-4e68-b3b7-0df95a0de725" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "70e9f6d7-14fb-4221-9dc4-45fe2167cf0c", "a892b811-80bd-4e68-b3b7-0df95a0de725" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "96ee7040-dbcd-44d4-a18f-d8b58eb3206f", "a892b811-80bd-4e68-b3b7-0df95a0de725" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "db2309f0-c714-498f-9c34-2684143f3929", "a892b811-80bd-4e68-b3b7-0df95a0de725" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70e9f6d7-14fb-4221-9dc4-45fe2167cf0c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "96ee7040-dbcd-44d4-a18f-d8b58eb3206f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "db2309f0-c714-498f-9c34-2684143f3929");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a892b811-80bd-4e68-b3b7-0df95a0de725");
        }
    }
}

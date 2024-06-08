using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medicare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ImageRouteAddedToDoctorIDCardLengthExtended : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("0b6e5f31-bfed-4868-ae57-d6096651cc8e"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("6d32c5d5-b619-46c0-9d04-764c2844f359"));

            migrationBuilder.AlterColumn<string>(
                name: "IdentityCard",
                table: "Doctors",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("a20b5c65-f913-4260-9d1e-c38b7c0e21a6"), "Asistente" },
                    { new Guid("bf59c74f-ee68-41c8-a72e-d9f053217b3a"), "Administrador" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a20b5c65-f913-4260-9d1e-c38b7c0e21a6"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bf59c74f-ee68-41c8-a72e-d9f053217b3a"));

            migrationBuilder.AlterColumn<string>(
                name: "IdentityCard",
                table: "Doctors",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0b6e5f31-bfed-4868-ae57-d6096651cc8e"), "Asistente" },
                    { new Guid("6d32c5d5-b619-46c0-9d04-764c2844f359"), "Administrador" }
                });
        }
    }
}

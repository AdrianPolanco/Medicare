using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medicare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ImageRouteAddedToDoctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("418cd9fc-aa79-4568-8431-64ac72c40362"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a1916a07-2edb-4445-b8c9-8631f6ca8bde"));

            migrationBuilder.AddColumn<string>(
                name: "ImageRoute",
                table: "Doctors",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0b6e5f31-bfed-4868-ae57-d6096651cc8e"), "Asistente" },
                    { new Guid("6d32c5d5-b619-46c0-9d04-764c2844f359"), "Administrador" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("0b6e5f31-bfed-4868-ae57-d6096651cc8e"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("6d32c5d5-b619-46c0-9d04-764c2844f359"));

            migrationBuilder.DropColumn(
                name: "ImageRoute",
                table: "Doctors");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("418cd9fc-aa79-4568-8431-64ac72c40362"), "Asistente" },
                    { new Guid("a1916a07-2edb-4445-b8c9-8631f6ca8bde"), "Administrador" }
                });
        }
    }
}

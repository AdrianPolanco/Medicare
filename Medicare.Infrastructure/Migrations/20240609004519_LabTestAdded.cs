using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medicare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LabTestAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a20b5c65-f913-4260-9d1e-c38b7c0e21a6"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bf59c74f-ee68-41c8-a72e-d9f053217b3a"));

            migrationBuilder.CreateTable(
                name: "LabTests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabTests", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "LabTests",
                columns: new[] { "Id", "Deleted", "Name" },
                values: new object[,]
                {
                    { new Guid("67abdd7e-3994-4092-8ca5-a963c8e3fe14"), false, "MRI" },
                    { new Guid("a2e8b7dd-a78e-4055-b4bd-0155e1ce09e7"), false, "Urine Test" },
                    { new Guid("c5cadd91-6713-4eac-b7df-2e34ce7f83e0"), false, "Blood Test" },
                    { new Guid("c80b85a3-d93b-4c4b-9598-c7597a510ec1"), false, "CT Scan" },
                    { new Guid("ecc7fea5-80c8-4741-926e-4b35a74e6dbf"), false, "X-Ray" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("977b1fbd-52c7-4b7a-beaa-6e7c6314744c"), "Administrador" },
                    { new Guid("f2396d86-f5ff-4e8d-ad30-6c8d21ab0727"), "Asistente" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabTests");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("977b1fbd-52c7-4b7a-beaa-6e7c6314744c"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f2396d86-f5ff-4e8d-ad30-6c8d21ab0727"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("a20b5c65-f913-4260-9d1e-c38b7c0e21a6"), "Asistente" },
                    { new Guid("bf59c74f-ee68-41c8-a72e-d9f053217b3a"), "Administrador" }
                });
        }
    }
}

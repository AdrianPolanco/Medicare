using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medicare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LabTestAddedWithRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: new Guid("67abdd7e-3994-4092-8ca5-a963c8e3fe14"));

            migrationBuilder.DeleteData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: new Guid("a2e8b7dd-a78e-4055-b4bd-0155e1ce09e7"));

            migrationBuilder.DeleteData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: new Guid("c5cadd91-6713-4eac-b7df-2e34ce7f83e0"));

            migrationBuilder.DeleteData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: new Guid("c80b85a3-d93b-4c4b-9598-c7597a510ec1"));

            migrationBuilder.DeleteData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: new Guid("ecc7fea5-80c8-4741-926e-4b35a74e6dbf"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("977b1fbd-52c7-4b7a-beaa-6e7c6314744c"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f2396d86-f5ff-4e8d-ad30-6c8d21ab0727"));

            migrationBuilder.AddColumn<Guid>(
                name: "OfficeId",
                table: "LabTests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("77d9fc01-66a8-410f-b46d-8e1481c06a66"), "Asistente" },
                    { new Guid("92a3afb7-2b25-4741-abdb-4478f8353eb4"), "Administrador" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LabTests_OfficeId",
                table: "LabTests",
                column: "OfficeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabTests_Offices_OfficeId",
                table: "LabTests",
                column: "OfficeId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabTests_Offices_OfficeId",
                table: "LabTests");

            migrationBuilder.DropIndex(
                name: "IX_LabTests_OfficeId",
                table: "LabTests");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("77d9fc01-66a8-410f-b46d-8e1481c06a66"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("92a3afb7-2b25-4741-abdb-4478f8353eb4"));

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "LabTests");

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
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medicare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PatientRelationOfficeAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2fa5205a-6c71-44f3-9f11-1f460f59895a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c689156c-6fec-4f8f-b882-38a33592d105"));

            migrationBuilder.AddColumn<Guid>(
                name: "OfficeId",
                table: "Patients",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0a8f9753-537c-43e2-bc8e-82a7ae084627"), "Administrador" },
                    { new Guid("86fdb1ec-4813-4e65-8f24-3c36d19a3a24"), "Asistente" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_OfficeId",
                table: "Patients",
                column: "OfficeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Offices_OfficeId",
                table: "Patients",
                column: "OfficeId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Offices_OfficeId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_OfficeId",
                table: "Patients");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("0a8f9753-537c-43e2-bc8e-82a7ae084627"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("86fdb1ec-4813-4e65-8f24-3c36d19a3a24"));

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "Patients");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2fa5205a-6c71-44f3-9f11-1f460f59895a"), "Administrador" },
                    { new Guid("c689156c-6fec-4f8f-b882-38a33592d105"), "Asistente" }
                });
        }
    }
}

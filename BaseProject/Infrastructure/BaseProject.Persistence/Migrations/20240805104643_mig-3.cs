using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseProject.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("45ce42d0-4ee4-4ade-902e-c2ce7eef6b53"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 5, 13, 46, 42, 537, DateTimeKind.Local).AddTicks(2561));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("9c3ea609-d3e7-4b66-b326-e786f3cbe745"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 5, 13, 46, 42, 537, DateTimeKind.Local).AddTicks(2544));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c1926f23-aa70-475b-95b0-ebe6d1600b7e"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 5, 13, 46, 42, 538, DateTimeKind.Local).AddTicks(1712));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d4144813-0e1f-4312-b82d-cc56336eb6da"),
                column: "CreatedDate",
                value: new DateTime(2024, 8, 5, 13, 46, 42, 538, DateTimeKind.Local).AddTicks(1724));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("45ce42d0-4ee4-4ade-902e-c2ce7eef6b53"),
                column: "CreatedDate",
                value: new DateTime(2024, 5, 20, 22, 0, 50, 332, DateTimeKind.Local).AddTicks(2975));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("9c3ea609-d3e7-4b66-b326-e786f3cbe745"),
                column: "CreatedDate",
                value: new DateTime(2024, 5, 20, 22, 0, 50, 332, DateTimeKind.Local).AddTicks(2958));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c1926f23-aa70-475b-95b0-ebe6d1600b7e"),
                column: "CreatedDate",
                value: new DateTime(2024, 5, 20, 22, 0, 50, 333, DateTimeKind.Local).AddTicks(646));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d4144813-0e1f-4312-b82d-cc56336eb6da"),
                column: "CreatedDate",
                value: new DateTime(2024, 5, 20, 22, 0, 50, 333, DateTimeKind.Local).AddTicks(657));
        }
    }
}

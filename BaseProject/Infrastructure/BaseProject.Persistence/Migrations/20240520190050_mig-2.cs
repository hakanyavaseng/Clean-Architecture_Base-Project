using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BaseProject.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "CreatedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("45ce42d0-4ee4-4ade-902e-c2ce7eef6b53"), new DateTime(2024, 5, 20, 22, 0, 50, 332, DateTimeKind.Local).AddTicks(2975), "Brand 2" },
                    { new Guid("9c3ea609-d3e7-4b66-b326-e786f3cbe745"), new DateTime(2024, 5, 20, 22, 0, 50, 332, DateTimeKind.Local).AddTicks(2958), "Brand 1" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CreatedDate", "Description", "Discount", "Price", "Title" },
                values: new object[,]
                {
                    { new Guid("c1926f23-aa70-475b-95b0-ebe6d1600b7e"), new Guid("9c3ea609-d3e7-4b66-b326-e786f3cbe745"), new DateTime(2024, 5, 20, 22, 0, 50, 333, DateTimeKind.Local).AddTicks(646), "Description 1", 0m, 100m, "Product 1" },
                    { new Guid("d4144813-0e1f-4312-b82d-cc56336eb6da"), new Guid("45ce42d0-4ee4-4ade-902e-c2ce7eef6b53"), new DateTime(2024, 5, 20, 22, 0, 50, 333, DateTimeKind.Local).AddTicks(657), "Description 2", 0m, 200m, "Product 2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c1926f23-aa70-475b-95b0-ebe6d1600b7e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d4144813-0e1f-4312-b82d-cc56336eb6da"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("45ce42d0-4ee4-4ade-902e-c2ce7eef6b53"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("9c3ea609-d3e7-4b66-b326-e786f3cbe745"));
        }
    }
}

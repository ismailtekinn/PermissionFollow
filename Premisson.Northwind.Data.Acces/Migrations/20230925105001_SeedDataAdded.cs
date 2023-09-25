using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Premisson.Northwind.Data.Acces.Migrations
{
    public partial class SeedDataAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DayoffTypes",
                columns: new[] { "Id", "IsDelete", "Name" },
                values: new object[,]
                {
                    { 1, false, "Yıllık İzin" },
                    { 2, false, "Hastalık İzin" },
                    { 3, false, "Mazeret İzin" }
                });

            migrationBuilder.InsertData(
                table: "Deparments",
                columns: new[] { "Id", "CreatedAt", "IsDelete", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 9, 25, 13, 50, 1, 626, DateTimeKind.Local).AddTicks(1326), false, "Muhasebe Birimi" },
                    { 2, new DateTime(2023, 9, 25, 13, 50, 1, 626, DateTimeKind.Local).AddTicks(9303), false, "Yazılım Birimi" },
                    { 3, new DateTime(2023, 9, 25, 13, 50, 1, 626, DateTimeKind.Local).AddTicks(9332), false, "Satış Birimi" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "IsDelete", "Name" },
                values: new object[,]
                {
                    { 1, false, "Admin" },
                    { 2, false, "Yönetici" },
                    { 3, false, "Personel" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "IsDelete", "Name", "Password", "RoleId", "Surname" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@corporate.com", true, false, "Admin", "1234", 1, "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DayoffTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DayoffTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DayoffTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Deparments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Deparments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Deparments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}

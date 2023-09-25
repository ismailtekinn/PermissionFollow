using Microsoft.EntityFrameworkCore.Migrations;

namespace Premisson.Northwind.Data.Acces.Migrations
{
    public partial class AddedForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDepartments_DepartmentId",
                table: "UserDepartments",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDepartments_UserId",
                table: "UserDepartments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Dayoffs_DayoffTypeId",
                table: "Dayoffs",
                column: "DayoffTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Dayoffs_ProxyUserId",
                table: "Dayoffs",
                column: "ProxyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Dayoffs_UserId",
                table: "Dayoffs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dayoffs_DayoffTypes_DayoffTypeId",
                table: "Dayoffs",
                column: "DayoffTypeId",
                principalTable: "DayoffTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dayoffs_Users_ProxyUserId",
                table: "Dayoffs",
                column: "ProxyUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dayoffs_Users_UserId",
                table: "Dayoffs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDepartments_Deparments_DepartmentId",
                table: "UserDepartments",
                column: "DepartmentId",
                principalTable: "Deparments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDepartments_Users_UserId",
                table: "UserDepartments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dayoffs_DayoffTypes_DayoffTypeId",
                table: "Dayoffs");

            migrationBuilder.DropForeignKey(
                name: "FK_Dayoffs_Users_ProxyUserId",
                table: "Dayoffs");

            migrationBuilder.DropForeignKey(
                name: "FK_Dayoffs_Users_UserId",
                table: "Dayoffs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDepartments_Deparments_DepartmentId",
                table: "UserDepartments");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDepartments_Users_UserId",
                table: "UserDepartments");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_UserDepartments_DepartmentId",
                table: "UserDepartments");

            migrationBuilder.DropIndex(
                name: "IX_UserDepartments_UserId",
                table: "UserDepartments");

            migrationBuilder.DropIndex(
                name: "IX_Dayoffs_DayoffTypeId",
                table: "Dayoffs");

            migrationBuilder.DropIndex(
                name: "IX_Dayoffs_ProxyUserId",
                table: "Dayoffs");

            migrationBuilder.DropIndex(
                name: "IX_Dayoffs_UserId",
                table: "Dayoffs");
        }
    }
}

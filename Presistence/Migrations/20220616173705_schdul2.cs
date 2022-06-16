using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Migrations
{
    public partial class schdul2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_schedules_Businesses_businessId",
                table: "schedules");

            migrationBuilder.RenameColumn(
                name: "businessId",
                table: "schedules",
                newName: "BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_schedules_businessId",
                table: "schedules",
                newName: "IX_schedules_BusinessId");

            migrationBuilder.AlterColumn<Guid>(
                name: "BusinessId",
                table: "schedules",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_schedules_Businesses_BusinessId",
                table: "schedules",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_schedules_Businesses_BusinessId",
                table: "schedules");

            migrationBuilder.RenameColumn(
                name: "BusinessId",
                table: "schedules",
                newName: "businessId");

            migrationBuilder.RenameIndex(
                name: "IX_schedules_BusinessId",
                table: "schedules",
                newName: "IX_schedules_businessId");

            migrationBuilder.AlterColumn<Guid>(
                name: "businessId",
                table: "schedules",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_schedules_Businesses_businessId",
                table: "schedules",
                column: "businessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

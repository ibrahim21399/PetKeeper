using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Migrations
{
    public partial class k : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessServices",
                table: "BusinessServices");

            migrationBuilder.DropIndex(
                name: "IX_BusinessServices_ServiceId",
                table: "BusinessServices");

            migrationBuilder.DropColumn(
                name: "ServicesId",
                table: "BusinessServices");

            migrationBuilder.DropColumn(
                name: "BusinessesId",
                table: "BusinessServices");
            
            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessServices",
                table: "BusinessServices",
                columns: new[] { "ServiceId", "BusinessId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessServices",
                table: "BusinessServices");

            migrationBuilder.AddColumn<Guid>(
                name: "ServicesId",
                table: "BusinessServices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessesId",
                table: "BusinessServices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessServices",
                table: "BusinessServices",
                columns: new[] { "ServicesId", "BusinessesId" });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessServices_ServiceId",
                table: "BusinessServices",
                column: "ServiceId");
        }
    }
}

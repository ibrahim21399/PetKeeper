using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Migrations
{
    public partial class nn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_AspNetUsers_ApplicationUserId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Businesses_BusinessId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_schedules_ScheduleId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_schedules_Businesses_BusinessId",
                table: "schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_schedules",
                table: "schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Booking",
                table: "Booking");

            migrationBuilder.RenameTable(
                name: "schedules",
                newName: "Schedules");

            migrationBuilder.RenameTable(
                name: "Booking",
                newName: "Bookings");

            migrationBuilder.RenameIndex(
                name: "IX_schedules_BusinessId",
                table: "Schedules",
                newName: "IX_Schedules_BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_ScheduleId",
                table: "Bookings",
                newName: "IX_Bookings_ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_BusinessId",
                table: "Bookings",
                newName: "IX_Bookings_BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_ApplicationUserId",
                table: "Bookings",
                newName: "IX_Bookings_ApplicationUserId");

            migrationBuilder.AddColumn<bool>(
                name: "IsCanceled",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AspNetUsers_ApplicationUserId",
                table: "Bookings",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Businesses_BusinessId",
                table: "Bookings",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Schedules_ScheduleId",
                table: "Bookings",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Businesses_BusinessId",
                table: "Schedules",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AspNetUsers_ApplicationUserId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Businesses_BusinessId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Schedules_ScheduleId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Businesses_BusinessId",
                table: "Schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "IsCanceled",
                table: "Bookings");

            migrationBuilder.RenameTable(
                name: "Schedules",
                newName: "schedules");

            migrationBuilder.RenameTable(
                name: "Bookings",
                newName: "Booking");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_BusinessId",
                table: "schedules",
                newName: "IX_schedules_BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_ScheduleId",
                table: "Booking",
                newName: "IX_Booking_ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_BusinessId",
                table: "Booking",
                newName: "IX_Booking_BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_ApplicationUserId",
                table: "Booking",
                newName: "IX_Booking_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_schedules",
                table: "schedules",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Booking",
                table: "Booking",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_AspNetUsers_ApplicationUserId",
                table: "Booking",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Businesses_BusinessId",
                table: "Booking",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_schedules_ScheduleId",
                table: "Booking",
                column: "ScheduleId",
                principalTable: "schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_schedules_Businesses_BusinessId",
                table: "schedules",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

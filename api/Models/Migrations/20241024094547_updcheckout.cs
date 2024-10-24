using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ebooking.Migrations
{
    /// <inheritdoc />
    public partial class updcheckout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Users_AppLicationUserId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckoutSessions_Events_EventId",
                table: "CheckoutSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckoutSessions_Users_AppLicationUserId",
                table: "CheckoutSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_CouponCodes_Events_EventId",
                table: "CouponCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_EventImages_Events_EventID",
                table: "EventImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_ApplicationUserID",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentInformation_CheckoutSessions_CheckoutSessionId",
                table: "PaymentInformation");

            migrationBuilder.DropIndex(
                name: "IX_CheckoutSessions_AppLicationUserId",
                table: "CheckoutSessions");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_AppLicationUserId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "AppLicationUserId",
                table: "CheckoutSessions");

            migrationBuilder.DropColumn(
                name: "PaymentInformationId",
                table: "CheckoutSessions");

            migrationBuilder.DropColumn(
                name: "AppLicationUserId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserID",
                table: "Events",
                newName: "AppUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Events_ApplicationUserID",
                table: "Events",
                newName: "IX_Events_AppUserID");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserID",
                table: "CheckoutSessions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserID",
                table: "Bookings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_CheckoutSessions_AppUserID",
                table: "CheckoutSessions",
                column: "AppUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_AppUserID",
                table: "Bookings",
                column: "AppUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Users_AppUserID",
                table: "Bookings",
                column: "AppUserID",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckoutSessions_Events_EventId",
                table: "CheckoutSessions",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckoutSessions_Users_AppUserID",
                table: "CheckoutSessions",
                column: "AppUserID",
                principalTable: "Users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_CouponCodes_Events_EventId",
                table: "CouponCodes",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventImages_Events_EventID",
                table: "EventImages",
                column: "EventID",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_AppUserID",
                table: "Events",
                column: "AppUserID",
                principalTable: "Users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentInformation_CheckoutSessions_CheckoutSessionId",
                table: "PaymentInformation",
                column: "CheckoutSessionId",
                principalTable: "CheckoutSessions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Users_AppUserID",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckoutSessions_Events_EventId",
                table: "CheckoutSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckoutSessions_Users_AppUserID",
                table: "CheckoutSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_CouponCodes_Events_EventId",
                table: "CouponCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_EventImages_Events_EventID",
                table: "EventImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_AppUserID",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentInformation_CheckoutSessions_CheckoutSessionId",
                table: "PaymentInformation");

            migrationBuilder.DropIndex(
                name: "IX_CheckoutSessions_AppUserID",
                table: "CheckoutSessions");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_AppUserID",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "AppUserID",
                table: "Events",
                newName: "ApplicationUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Events_AppUserID",
                table: "Events",
                newName: "IX_Events_ApplicationUserID");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserID",
                table: "CheckoutSessions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "AppLicationUserId",
                table: "CheckoutSessions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentInformationId",
                table: "CheckoutSessions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "AppUserID",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "AppLicationUserId",
                table: "Bookings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CheckoutSessions_AppLicationUserId",
                table: "CheckoutSessions",
                column: "AppLicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_AppLicationUserId",
                table: "Bookings",
                column: "AppLicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Users_AppLicationUserId",
                table: "Bookings",
                column: "AppLicationUserId",
                principalTable: "Users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckoutSessions_Events_EventId",
                table: "CheckoutSessions",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckoutSessions_Users_AppLicationUserId",
                table: "CheckoutSessions",
                column: "AppLicationUserId",
                principalTable: "Users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_CouponCodes_Events_EventId",
                table: "CouponCodes",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventImages_Events_EventID",
                table: "EventImages",
                column: "EventID",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_ApplicationUserID",
                table: "Events",
                column: "ApplicationUserID",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentInformation_CheckoutSessions_CheckoutSessionId",
                table: "PaymentInformation",
                column: "CheckoutSessionId",
                principalTable: "CheckoutSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

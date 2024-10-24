using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ebooking.Migrations
{
    /// <inheritdoc />
    public partial class modifiedcheckout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Events_EventId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentInformation_Bookings_BookingId",
                table: "PaymentInformation");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_EventId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "TicketInformation_Capacity",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "TotalTicketsPurchased",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "BookingId",
                table: "PaymentInformation",
                newName: "CheckoutSessionId");

            migrationBuilder.RenameColumn(
                name: "AmountPaid",
                table: "PaymentInformation",
                newName: "AmountToBePaid");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentInformation_BookingId",
                table: "PaymentInformation",
                newName: "IX_PaymentInformation_CheckoutSessionId");

            migrationBuilder.RenameColumn(
                name: "PaymentInformationId",
                table: "Bookings",
                newName: "CheckoutSessionId");

            migrationBuilder.AlterColumn<Guid>(
                name: "TransactionId",
                table: "PaymentInformation",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "PaymentInformation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "EventsId",
                table: "Bookings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CheckoutSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppUserID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppLicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalTicketsPurchased = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CouponCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FinalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentInformationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckoutSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckoutSessions_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheckoutSessions_Users_AppLicationUserId",
                        column: x => x.AppLicationUserId,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    CheckoutSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TicketName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => new { x.CheckoutSessionId, x.Id });
                    table.ForeignKey(
                        name: "FK_Tickets_CheckoutSessions_CheckoutSessionId",
                        column: x => x.CheckoutSessionId,
                        principalTable: "CheckoutSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CheckoutSessionId",
                table: "Bookings",
                column: "CheckoutSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_EventsId",
                table: "Bookings",
                column: "EventsId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckoutSessions_AppLicationUserId",
                table: "CheckoutSessions",
                column: "AppLicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckoutSessions_EventId",
                table: "CheckoutSessions",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_CheckoutSessions_CheckoutSessionId",
                table: "Bookings",
                column: "CheckoutSessionId",
                principalTable: "CheckoutSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Events_EventsId",
                table: "Bookings",
                column: "EventsId",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentInformation_CheckoutSessions_CheckoutSessionId",
                table: "PaymentInformation",
                column: "CheckoutSessionId",
                principalTable: "CheckoutSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_CheckoutSessions_CheckoutSessionId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Events_EventsId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentInformation_CheckoutSessions_CheckoutSessionId",
                table: "PaymentInformation");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "CheckoutSessions");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_CheckoutSessionId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_EventsId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "PaymentInformation");

            migrationBuilder.DropColumn(
                name: "EventsId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "CheckoutSessionId",
                table: "PaymentInformation",
                newName: "BookingId");

            migrationBuilder.RenameColumn(
                name: "AmountToBePaid",
                table: "PaymentInformation",
                newName: "AmountPaid");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentInformation_CheckoutSessionId",
                table: "PaymentInformation",
                newName: "IX_PaymentInformation_BookingId");

            migrationBuilder.RenameColumn(
                name: "CheckoutSessionId",
                table: "Bookings",
                newName: "PaymentInformationId");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionId",
                table: "PaymentInformation",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Bookings",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "EventId",
                table: "Bookings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Bookings",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TicketInformation_Capacity",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalTicketsPurchased",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_EventId",
                table: "Bookings",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Events_EventId",
                table: "Bookings",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentInformation_Bookings_BookingId",
                table: "PaymentInformation",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ebooking.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Users_AppLicationUserId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_ApplicationUserID",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_ApplicationUserID",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_AppLicationUserId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ApplicationUserID",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AppLicationUserId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "AppUserID",
                table: "Bookings",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "PhoneNumber",
                table: "Bookings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Bookings",
                newName: "AppUserID");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserID",
                table: "Events",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AppLicationUserId",
                table: "Bookings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_ApplicationUserID",
                table: "Events",
                column: "ApplicationUserID");

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
                name: "FK_Events_Users_ApplicationUserID",
                table: "Events",
                column: "ApplicationUserID",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

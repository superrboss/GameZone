using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameZone.Migrations
{
    /// <inheritdoc />
    public partial class @try : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamesDevices_Devices_deviceId",
                table: "GamesDevices");

            migrationBuilder.RenameColumn(
                name: "deviceId",
                table: "GamesDevices",
                newName: "DeviceId");

            migrationBuilder.RenameIndex(
                name: "IX_GamesDevices_deviceId",
                table: "GamesDevices",
                newName: "IX_GamesDevices_DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_GamesDevices_Devices_DeviceId",
                table: "GamesDevices",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamesDevices_Devices_DeviceId",
                table: "GamesDevices");

            migrationBuilder.RenameColumn(
                name: "DeviceId",
                table: "GamesDevices",
                newName: "deviceId");

            migrationBuilder.RenameIndex(
                name: "IX_GamesDevices_DeviceId",
                table: "GamesDevices",
                newName: "IX_GamesDevices_deviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_GamesDevices_Devices_deviceId",
                table: "GamesDevices",
                column: "deviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

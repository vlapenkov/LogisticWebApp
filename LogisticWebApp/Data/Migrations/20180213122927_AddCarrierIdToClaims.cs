using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LogisticWebApp.Data.Migrations
{
    public partial class AddCarrierIdToClaims : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CarrierId",
                table: "ClaimsForTransport",
                maxLength: 7,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClaimsForTransport_CarrierId",
                table: "ClaimsForTransport",
                column: "CarrierId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClaimsForTransport_Carriers_CarrierId",
                table: "ClaimsForTransport",
                column: "CarrierId",
                principalTable: "Carriers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClaimsForTransport_Carriers_CarrierId",
                table: "ClaimsForTransport");

            migrationBuilder.DropIndex(
                name: "IX_ClaimsForTransport_CarrierId",
                table: "ClaimsForTransport");

            migrationBuilder.DropColumn(
                name: "CarrierId",
                table: "ClaimsForTransport");
        }
    }
}

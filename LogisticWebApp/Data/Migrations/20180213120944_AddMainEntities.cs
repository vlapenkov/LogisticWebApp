using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LogisticWebApp.Data.Migrations
{
    public partial class AddMainEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "CarrierId",
                table: "AspNetUsers",
                maxLength: 7,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fio",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Carriers",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 7, nullable: false),
                    FullName = table.Column<string>(maxLength: 255, nullable: false),
                    Inn = table.Column<string>(maxLength: 12, nullable: false),
                    Kpp = table.Column<string>(maxLength: 9, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carriers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClaimsForTransport",
                columns: table => new
                {
                    GuidIn1S = table.Column<Guid>(nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    DeadlineDate = table.Column<DateTime>(nullable: true),
                    DocDate = table.Column<DateTime>(nullable: false),
                    NumberIn1S = table.Column<string>(maxLength: 10, nullable: false),
                    Path = table.Column<string>(nullable: false),
                    ReadyDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Volume = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimsForTransport", x => x.GuidIn1S);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CarrierId = table.Column<string>(maxLength: 7, nullable: true),
                    Fio = table.Column<string>(maxLength: 100, nullable: false),
                    HasContract = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drivers_Carriers_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "Carriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Brand = table.Column<string>(maxLength: 50, nullable: false),
                    CarrierId = table.Column<string>(maxLength: 7, nullable: true),
                    DriverId = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Model = table.Column<string>(maxLength: 50, nullable: false),
                    StateNumber = table.Column<string>(maxLength: 50, nullable: false),
                    Volume = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Carriers_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "Carriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cars_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RepliesToClaims",
                columns: table => new
                {
                    GuidOfClaimIn1S = table.Column<Guid>(nullable: false),
                    CarrierId = table.Column<string>(maxLength: 7, nullable: false),
                    ArrivalDate = table.Column<DateTime>(nullable: true),
                    CarId = table.Column<int>(nullable: true),
                    Cost = table.Column<decimal>(nullable: false),
                    DriverId = table.Column<int>(nullable: true),
                    UnloadDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepliesToClaims", x => new { x.GuidOfClaimIn1S, x.CarrierId });
                    table.UniqueConstraint("AK_RepliesToClaims_CarrierId_GuidOfClaimIn1S", x => new { x.CarrierId, x.GuidOfClaimIn1S });
                    table.ForeignKey(
                        name: "FK_RepliesToClaims_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RepliesToClaims_Carriers_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "Carriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepliesToClaims_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RepliesToClaims_ClaimsForTransport_GuidOfClaimIn1S",
                        column: x => x.GuidOfClaimIn1S,
                        principalTable: "ClaimsForTransport",
                        principalColumn: "GuidIn1S",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CarrierId",
                table: "AspNetUsers",
                column: "CarrierId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarrierId",
                table: "Cars",
                column: "CarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_DriverId",
                table: "Cars",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_CarrierId",
                table: "Drivers",
                column: "CarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_RepliesToClaims_CarId",
                table: "RepliesToClaims",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_RepliesToClaims_DriverId",
                table: "RepliesToClaims",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Carriers_CarrierId",
                table: "AspNetUsers",
                column: "CarrierId",
                principalTable: "Carriers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Carriers_CarrierId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "RepliesToClaims");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "ClaimsForTransport");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Carriers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CarrierId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CarrierId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Fio",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }
    }
}

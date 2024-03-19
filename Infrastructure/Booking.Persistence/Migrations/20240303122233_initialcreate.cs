using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "hotels",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HotelName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hotels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "billingpayments",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReservationId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HotelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BillingType = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_billingpayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_billingpayments_hotels_HotelId",
                        column: x => x.HotelId,
                        principalSchema: "dbo",
                        principalTable: "hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hotelrooms",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HotelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hotelrooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_hotelrooms_hotels_HotelId",
                        column: x => x.HotelId,
                        principalSchema: "dbo",
                        principalTable: "hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hotelroomprices",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HotelRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HotelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomType = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hotelroomprices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_hotelroomprices_hotelrooms_HotelRoomId",
                        column: x => x.HotelRoomId,
                        principalSchema: "dbo",
                        principalTable: "hotelrooms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_hotelroomprices_hotels_HotelId",
                        column: x => x.HotelId,
                        principalSchema: "dbo",
                        principalTable: "hotels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_billingpayments_HotelId",
                schema: "dbo",
                table: "billingpayments",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_hotelroomprices_HotelId",
                schema: "dbo",
                table: "hotelroomprices",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_hotelroomprices_HotelRoomId",
                schema: "dbo",
                table: "hotelroomprices",
                column: "HotelRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_hotelrooms_HotelId",
                schema: "dbo",
                table: "hotelrooms",
                column: "HotelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "billingpayments",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "hotelroomprices",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "hotelrooms",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "hotels",
                schema: "dbo");
        }
    }
}

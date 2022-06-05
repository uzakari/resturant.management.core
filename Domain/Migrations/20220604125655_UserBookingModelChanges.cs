using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    public partial class UserBookingModelChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resturants_ResturantOwner_ResturantOwnerId",
                table: "Resturants");

            migrationBuilder.RenameColumn(
                name: "ResturantTable",
                table: "UserBookings",
                newName: "ResturantTableID");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPersons",
                table: "UserBookings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ReservationTime",
                table: "UserBookings",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AlterColumn<int>(
                name: "ResturantOwnerId",
                table: "Resturants",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Resturants_ResturantOwner_ResturantOwnerId",
                table: "Resturants",
                column: "ResturantOwnerId",
                principalTable: "ResturantOwner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resturants_ResturantOwner_ResturantOwnerId",
                table: "Resturants");

            migrationBuilder.DropColumn(
                name: "NumberOfPersons",
                table: "UserBookings");

            migrationBuilder.DropColumn(
                name: "ReservationTime",
                table: "UserBookings");

            migrationBuilder.RenameColumn(
                name: "ResturantTableID",
                table: "UserBookings",
                newName: "ResturantTable");

            migrationBuilder.AlterColumn<int>(
                name: "ResturantOwnerId",
                table: "Resturants",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_ResturantOwner_Email",
                table: "ResturantOwner",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Resturants_ResturantOwner_ResturantOwnerId",
                table: "Resturants",
                column: "ResturantOwnerId",
                principalTable: "ResturantOwner",
                principalColumn: "Id");
        }
    }
}

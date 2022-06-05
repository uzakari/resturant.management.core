using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResturantOwner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResturantOwner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserBookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    ResturantID = table.Column<int>(type: "INTEGER", nullable: false),
                    ResturantTable = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBookings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resturants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ResturantOwnerId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resturants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resturants_ResturantOwner_ResturantOwnerId",
                        column: x => x.ResturantOwnerId,
                        principalTable: "ResturantOwner",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ResturantTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumberOfSeat = table.Column<int>(type: "INTEGER", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: true),
                    Available = table.Column<bool>(type: "INTEGER", nullable: false),
                    ResturantId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResturantTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResturantTable_Resturants_ResturantId",
                        column: x => x.ResturantId,
                        principalTable: "Resturants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resturants_ResturantOwnerId",
                table: "Resturants",
                column: "ResturantOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ResturantTable_ResturantId",
                table: "ResturantTable",
                column: "ResturantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResturantTable");

            migrationBuilder.DropTable(
                name: "UserBookings");

            migrationBuilder.DropTable(
                name: "Resturants");

            migrationBuilder.DropTable(
                name: "ResturantOwner");
        }
    }
}

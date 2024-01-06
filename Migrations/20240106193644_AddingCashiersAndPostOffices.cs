using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddingCashiersAndPostOffices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostOfficeID",
                table: "Package",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostOfficeID",
                table: "Courier",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PostOffice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    County = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostOffice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cashier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CashierFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CashierLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostOfficeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cashier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cashier_PostOffice_PostOfficeID",
                        column: x => x.PostOfficeID,
                        principalTable: "PostOffice",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Package_PostOfficeID",
                table: "Package",
                column: "PostOfficeID");

            migrationBuilder.CreateIndex(
                name: "IX_Courier_PostOfficeID",
                table: "Courier",
                column: "PostOfficeID");

            migrationBuilder.CreateIndex(
                name: "IX_Cashier_PostOfficeID",
                table: "Cashier",
                column: "PostOfficeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Courier_PostOffice_PostOfficeID",
                table: "Courier",
                column: "PostOfficeID",
                principalTable: "PostOffice",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Package_PostOffice_PostOfficeID",
                table: "Package",
                column: "PostOfficeID",
                principalTable: "PostOffice",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courier_PostOffice_PostOfficeID",
                table: "Courier");

            migrationBuilder.DropForeignKey(
                name: "FK_Package_PostOffice_PostOfficeID",
                table: "Package");

            migrationBuilder.DropTable(
                name: "Cashier");

            migrationBuilder.DropTable(
                name: "PostOffice");

            migrationBuilder.DropIndex(
                name: "IX_Package_PostOfficeID",
                table: "Package");

            migrationBuilder.DropIndex(
                name: "IX_Courier_PostOfficeID",
                table: "Courier");

            migrationBuilder.DropColumn(
                name: "PostOfficeID",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "PostOfficeID",
                table: "Courier");
        }
    }
}

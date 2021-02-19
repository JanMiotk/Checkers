using Microsoft.EntityFrameworkCore.Migrations;

namespace Depot.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    Nick = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "ID", "Link", "Name" },
                values: new object[,]
                {
                    { 1, "/Room/Master", "Master" },
                    { 2, "/Room/Average", "Average" },
                    { 3, "/Room/Novice", "Novice" },
                    { 4, "/Room/Beginner", "Beginner" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "Email", "Name", "Nick", "Password", "Surname" },
                values: new object[,]
                {
                    { 1, "janek@wp.pl", "Jan", "spacehunter", "password", "Kowalski" },
                    { 2, "kasia@wp.pl", "Katarzyna", "darkqueen", "password", "Ebut" },
                    { 3, "zenek@wp.pl", "Zenon", "imperator", "password", "Dacki" },
                    { 4, "seba@wp.pl", "Sebastian", "conqueror", "password", "Roden" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Practice.Migrations
{
    public partial class AddDbSets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bands",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    bandName = table.Column<string>(nullable: false),
                    musicGenre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bands", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "albums",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    albumTitle = table.Column<string>(nullable: false),
                    albumDesc = table.Column<string>(maxLength: 1000, nullable: false),
                    albumRating = table.Column<int>(nullable: false),
                    BandModelid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_albums", x => x.id);
                    table.ForeignKey(
                        name: "FK_albums_bands_BandModelid",
                        column: x => x.BandModelid,
                        principalTable: "bands",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_albums_BandModelid",
                table: "albums",
                column: "BandModelid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "albums");

            migrationBuilder.DropTable(
                name: "bands");
        }
    }
}

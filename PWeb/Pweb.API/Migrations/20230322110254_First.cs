using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Pweb.API.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "actori",
                columns: table => new
                {
                    actorid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nume = table.Column<string>(type: "text", nullable: false),
                    prenume = table.Column<string>(type: "text", nullable: false),
                    nationalitate = table.Column<string>(type: "text", nullable: false),
                    datanastere = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_actori", x => x.actorid);
                });

            migrationBuilder.CreateTable(
                name: "director",
                columns: table => new
                {
                    directorid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nume = table.Column<string>(type: "text", nullable: false),
                    prenume = table.Column<string>(type: "text", nullable: false),
                    varsta = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_director", x => x.directorid);
                });

            migrationBuilder.CreateTable(
                name: "genuri",
                columns: table => new
                {
                    genid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    numegen = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genuri", x => x.genid);
                });

            migrationBuilder.CreateTable(
                name: "scenariu",
                columns: table => new
                {
                    scenariuid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nume = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scenariu", x => x.scenariuid);
                });

            migrationBuilder.CreateTable(
                name: "scriitori",
                columns: table => new
                {
                    scriitorid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nume = table.Column<string>(type: "text", nullable: false),
                    prenume = table.Column<string>(type: "text", nullable: false),
                    varsta = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scriitori", x => x.scriitorid);
                });

            migrationBuilder.CreateTable(
                name: "filme",
                columns: table => new
                {
                    filmid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    directorid1 = table.Column<int>(type: "integer", nullable: false),
                    genid = table.Column<int>(type: "integer", nullable: false),
                    actorid = table.Column<int>(type: "integer", nullable: false),
                    scenariuid1 = table.Column<int>(type: "integer", nullable: false),
                    titlu = table.Column<string>(type: "text", nullable: false),
                    durata = table.Column<string>(type: "text", nullable: false),
                    descriere = table.Column<string>(type: "text", nullable: false),
                    anaparitie = table.Column<int>(type: "integer", nullable: false),
                    categorievarsta = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_filme", x => x.filmid);
                    table.ForeignKey(
                        name: "FK_filme_actori_actorid",
                        column: x => x.actorid,
                        principalTable: "actori",
                        principalColumn: "actorid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_filme_director_directorid1",
                        column: x => x.directorid1,
                        principalTable: "director",
                        principalColumn: "directorid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_filme_genuri_genid",
                        column: x => x.genid,
                        principalTable: "genuri",
                        principalColumn: "genid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_filme_scenariu_scenariuid1",
                        column: x => x.scenariuid1,
                        principalTable: "scenariu",
                        principalColumn: "scenariuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "scriitoriscenariu",
                columns: table => new
                {
                    scriitorid = table.Column<int>(type: "integer", nullable: false),
                    scenariuid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scriitoriscenariu", x => new { x.scriitorid, x.scenariuid });
                    table.ForeignKey(
                        name: "FK_scriitoriscenariu_scenariu_scenariuid",
                        column: x => x.scenariuid,
                        principalTable: "scenariu",
                        principalColumn: "scenariuid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_scriitoriscenariu_scriitori_scriitorid",
                        column: x => x.scriitorid,
                        principalTable: "scriitori",
                        principalColumn: "scriitorid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "filmeactori",
                columns: table => new
                {
                    filmid = table.Column<int>(type: "integer", nullable: false),
                    actorid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_filmeactori", x => new { x.filmid, x.actorid });
                    table.ForeignKey(
                        name: "FK_filmeactori_actori_actorid",
                        column: x => x.actorid,
                        principalTable: "actori",
                        principalColumn: "actorid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_filmeactori_filme_filmid",
                        column: x => x.filmid,
                        principalTable: "filme",
                        principalColumn: "filmid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "filmegenuri",
                columns: table => new
                {
                    filmid = table.Column<int>(type: "integer", nullable: false),
                    genid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_filmegenuri", x => new { x.filmid, x.genid });
                    table.ForeignKey(
                        name: "FK_filmegenuri_filme_filmid",
                        column: x => x.filmid,
                        principalTable: "filme",
                        principalColumn: "filmid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_filmegenuri_genuri_genid",
                        column: x => x.genid,
                        principalTable: "genuri",
                        principalColumn: "genid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_filme_actorid",
                table: "filme",
                column: "actorid");

            migrationBuilder.CreateIndex(
                name: "IX_filme_directorid1",
                table: "filme",
                column: "directorid1");

            migrationBuilder.CreateIndex(
                name: "IX_filme_genid",
                table: "filme",
                column: "genid");

            migrationBuilder.CreateIndex(
                name: "IX_filme_scenariuid1",
                table: "filme",
                column: "scenariuid1",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_filmeactori_actorid",
                table: "filmeactori",
                column: "actorid");

            migrationBuilder.CreateIndex(
                name: "IX_filmegenuri_genid",
                table: "filmegenuri",
                column: "genid");

            migrationBuilder.CreateIndex(
                name: "IX_scriitoriscenariu_scenariuid",
                table: "scriitoriscenariu",
                column: "scenariuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "filmeactori");

            migrationBuilder.DropTable(
                name: "filmegenuri");

            migrationBuilder.DropTable(
                name: "scriitoriscenariu");

            migrationBuilder.DropTable(
                name: "filme");

            migrationBuilder.DropTable(
                name: "scriitori");

            migrationBuilder.DropTable(
                name: "actori");

            migrationBuilder.DropTable(
                name: "director");

            migrationBuilder.DropTable(
                name: "genuri");

            migrationBuilder.DropTable(
                name: "scenariu");
        }
    }
}

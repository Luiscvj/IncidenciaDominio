using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistencia.Data.Migrations
{
    /// <inheritdoc />
    public partial class Pruebapersona_TipoPersona : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_persona_tipo_persona_TipoPersonaId",
                table: "persona");

            migrationBuilder.DropIndex(
                name: "IX_persona_TipoPersonaId",
                table: "persona");

            migrationBuilder.CreateTable(
                name: "persona_TipoPersonas",
                columns: table => new
                {
                    PersonasId = table.Column<string>(type: "varchar(20)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoPersonasIdTipoPersona = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persona_TipoPersonas", x => new { x.PersonasId, x.TipoPersonasIdTipoPersona });
                    table.ForeignKey(
                        name: "FK_persona_TipoPersonas_persona_PersonasId",
                        column: x => x.PersonasId,
                        principalTable: "persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_persona_TipoPersonas_tipo_persona_TipoPersonasIdTipoPersona",
                        column: x => x.TipoPersonasIdTipoPersona,
                        principalTable: "tipo_persona",
                        principalColumn: "IdTipoPersona",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_persona_TipoPersonas_TipoPersonasIdTipoPersona",
                table: "persona_TipoPersonas",
                column: "TipoPersonasIdTipoPersona");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "persona_TipoPersonas");

            migrationBuilder.CreateIndex(
                name: "IX_persona_TipoPersonaId",
                table: "persona",
                column: "TipoPersonaId");

            migrationBuilder.AddForeignKey(
                name: "FK_persona_tipo_persona_TipoPersonaId",
                table: "persona",
                column: "TipoPersonaId",
                principalTable: "tipo_persona",
                principalColumn: "IdTipoPersona",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIhackaton.Data.Migrations
{
    /// <inheritdoc />
    public partial class UnifyPerformanceAndTutors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Docentes_Cursos_CursoId",
                table: "Docentes");

            migrationBuilder.DropIndex(
                name: "IX_Docentes_CursoId",
                table: "Docentes");

            migrationBuilder.DropColumn(
                name: "CursoId",
                table: "Docentes");

            migrationBuilder.AddColumn<int>(
                name: "ContadorAlertas",
                table: "Tutores",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Performances",
                columns: table => new
                {
                    AlumnoDni = table.Column<int>(type: "INTEGER", nullable: false),
                    CursoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Inasistencias = table.Column<int>(type: "INTEGER", nullable: false),
                    ProgresoGeneral = table.Column<int>(type: "INTEGER", nullable: false),
                    Racha = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performances", x => new { x.AlumnoDni, x.CursoId });
                    table.ForeignKey(
                        name: "FK_Performances_Alumnos_AlumnoDni",
                        column: x => x.AlumnoDni,
                        principalTable: "Alumnos",
                        principalColumn: "dni",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Performances_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TutoresAlumnos",
                columns: table => new
                {
                    AlumnoDni = table.Column<int>(type: "INTEGER", nullable: false),
                    TutorDni = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutoresAlumnos", x => new { x.AlumnoDni, x.TutorDni });
                    table.ForeignKey(
                        name: "FK_TutoresAlumnos_Alumnos_AlumnoDni",
                        column: x => x.AlumnoDni,
                        principalTable: "Alumnos",
                        principalColumn: "dni",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutoresAlumnos_Tutores_TutorDni",
                        column: x => x.TutorDni,
                        principalTable: "Tutores",
                        principalColumn: "dni",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Performances_CursoId",
                table: "Performances",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_TutoresAlumnos_TutorDni",
                table: "TutoresAlumnos",
                column: "TutorDni");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Performances");

            migrationBuilder.DropTable(
                name: "TutoresAlumnos");

            migrationBuilder.DropColumn(
                name: "ContadorAlertas",
                table: "Tutores");

            migrationBuilder.AddColumn<int>(
                name: "CursoId",
                table: "Docentes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Docentes_CursoId",
                table: "Docentes",
                column: "CursoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Docentes_Cursos_CursoId",
                table: "Docentes",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id");
        }
    }
}

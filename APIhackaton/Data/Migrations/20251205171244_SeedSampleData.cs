using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIhackaton.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedSampleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alumnos_Cursos_CursoId",
                table: "Alumnos");

            migrationBuilder.DropIndex(
                name: "IX_Alumnos_CursoId",
                table: "Alumnos");

            migrationBuilder.DropColumn(
                name: "CursoId",
                table: "Alumnos");

            migrationBuilder.InsertData(
                table: "Alumnos",
                columns: new[] { "dni", "apellido", "email", "estado", "nombre", "rol" },
                values: new object[,]
                {
                    { 3001, "Lopez", "juan@student.com", true, "Juan", 1 },
                    { 3002, "Garcia", "maria@student.com", true, "María", 1 }
                });

            migrationBuilder.InsertData(
                table: "Docentes",
                columns: new[] { "dni", "apellido", "email", "estado", "nombre", "rol" },
                values: new object[,]
                {
                    { 1001, "Gomez", "carlos@gymail.com", true, "Carlos", 2 },
                    { 1002, "Perez", "ana@school.com", true, "Ana", 2 }
                });

            migrationBuilder.InsertData(
                table: "Materias",
                columns: new[] { "codigo", "nombre" },
                values: new object[,]
                {
                    { "FIS01", "Física" },
                    { "MAT01", "Matemáticas" }
                });

            migrationBuilder.InsertData(
                table: "Tutores",
                columns: new[] { "dni", "ContadorAlertas", "apellido", "email", "estado", "nombre", "rol" },
                values: new object[] { 2001, 0, "Ruiz", "laura@home.com", true, "Laura", 3 });

            migrationBuilder.InsertData(
                table: "Cursos",
                columns: new[] { "Id", "Anio", "Turno", "codigo", "comision", "dniDocente" },
                values: new object[,]
                {
                    { 1, 2025, "Mañana", "MAT01", 1, 1001 },
                    { 2, 2025, "Tarde", "FIS01", 2, 1002 }
                });

            migrationBuilder.InsertData(
                table: "TutoresAlumnos",
                columns: new[] { "AlumnoDni", "TutorDni" },
                values: new object[] { 3001, 2001 });

            migrationBuilder.InsertData(
                table: "Performances",
                columns: new[] { "AlumnoDni", "CursoId", "Inasistencias", "ProgresoGeneral", "Racha" },
                values: new object[,]
                {
                    { 3001, 1, 2, 70, 3 },
                    { 3002, 1, 0, 85, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_dniDocente",
                table: "Cursos",
                column: "dniDocente");

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_Docentes_dniDocente",
                table: "Cursos",
                column: "dniDocente",
                principalTable: "Docentes",
                principalColumn: "dni",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_Docentes_dniDocente",
                table: "Cursos");

            migrationBuilder.DropIndex(
                name: "IX_Cursos_dniDocente",
                table: "Cursos");

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Materias",
                keyColumn: "codigo",
                keyValue: "FIS01");

            migrationBuilder.DeleteData(
                table: "Materias",
                keyColumn: "codigo",
                keyValue: "MAT01");

            migrationBuilder.DeleteData(
                table: "Performances",
                keyColumns: new[] { "AlumnoDni", "CursoId" },
                keyValues: new object[] { 3001, 1 });

            migrationBuilder.DeleteData(
                table: "Performances",
                keyColumns: new[] { "AlumnoDni", "CursoId" },
                keyValues: new object[] { 3002, 1 });

            migrationBuilder.DeleteData(
                table: "TutoresAlumnos",
                keyColumns: new[] { "AlumnoDni", "TutorDni" },
                keyValues: new object[] { 3001, 2001 });

            migrationBuilder.DeleteData(
                table: "Alumnos",
                keyColumn: "dni",
                keyValue: 3001);

            migrationBuilder.DeleteData(
                table: "Alumnos",
                keyColumn: "dni",
                keyValue: 3002);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Docentes",
                keyColumn: "dni",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "Tutores",
                keyColumn: "dni",
                keyValue: 2001);

            migrationBuilder.DeleteData(
                table: "Docentes",
                keyColumn: "dni",
                keyValue: 1001);

            migrationBuilder.AddColumn<int>(
                name: "CursoId",
                table: "Alumnos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alumnos_CursoId",
                table: "Alumnos",
                column: "CursoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alumnos_Cursos_CursoId",
                table: "Alumnos",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id");
        }
    }
}

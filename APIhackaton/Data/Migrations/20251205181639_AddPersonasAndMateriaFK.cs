using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIhackaton.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonasAndMateriaFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "apellido",
                table: "Tutores");

            migrationBuilder.DropColumn(
                name: "email",
                table: "Tutores");

            migrationBuilder.DropColumn(
                name: "estado",
                table: "Tutores");

            migrationBuilder.DropColumn(
                name: "nombre",
                table: "Tutores");

            migrationBuilder.DropColumn(
                name: "rol",
                table: "Tutores");

            migrationBuilder.DropColumn(
                name: "apellido",
                table: "Docentes");

            migrationBuilder.DropColumn(
                name: "email",
                table: "Docentes");

            migrationBuilder.DropColumn(
                name: "estado",
                table: "Docentes");

            migrationBuilder.DropColumn(
                name: "nombre",
                table: "Docentes");

            migrationBuilder.DropColumn(
                name: "rol",
                table: "Docentes");

            migrationBuilder.DropColumn(
                name: "apellido",
                table: "Alumnos");

            migrationBuilder.DropColumn(
                name: "email",
                table: "Alumnos");

            migrationBuilder.DropColumn(
                name: "estado",
                table: "Alumnos");

            migrationBuilder.DropColumn(
                name: "nombre",
                table: "Alumnos");

            migrationBuilder.DropColumn(
                name: "rol",
                table: "Alumnos");

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    dni = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nombre = table.Column<string>(type: "TEXT", nullable: false),
                    apellido = table.Column<string>(type: "TEXT", nullable: false),
                    email = table.Column<string>(type: "TEXT", nullable: false),
                    rol = table.Column<int>(type: "INTEGER", nullable: false),
                    estado = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.dni);
                });

            migrationBuilder.InsertData(
                table: "Personas",
                columns: new[] { "dni", "apellido", "email", "estado", "nombre", "rol" },
                values: new object[,]
                {
                    { 1001, "Gomez", "carlos@gymail.com", true, "Carlos", 2 },
                    { 1002, "Perez", "ana@school.com", true, "Ana", 2 },
                    { 2001, "Ruiz", "laura@home.com", true, "Laura", 3 },
                    { 3001, "Lopez", "juan@student.com", true, "Juan", 1 },
                    { 3002, "Garcia", "maria@student.com", true, "María", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_codigo",
                table: "Cursos",
                column: "codigo");

            migrationBuilder.AddForeignKey(
                name: "FK_Alumnos_Personas_dni",
                table: "Alumnos",
                column: "dni",
                principalTable: "Personas",
                principalColumn: "dni",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_Materias_codigo",
                table: "Cursos",
                column: "codigo",
                principalTable: "Materias",
                principalColumn: "codigo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Docentes_Personas_dni",
                table: "Docentes",
                column: "dni",
                principalTable: "Personas",
                principalColumn: "dni",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tutores_Personas_dni",
                table: "Tutores",
                column: "dni",
                principalTable: "Personas",
                principalColumn: "dni",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alumnos_Personas_dni",
                table: "Alumnos");

            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_Materias_codigo",
                table: "Cursos");

            migrationBuilder.DropForeignKey(
                name: "FK_Docentes_Personas_dni",
                table: "Docentes");

            migrationBuilder.DropForeignKey(
                name: "FK_Tutores_Personas_dni",
                table: "Tutores");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropIndex(
                name: "IX_Cursos_codigo",
                table: "Cursos");

            migrationBuilder.AddColumn<string>(
                name: "apellido",
                table: "Tutores",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Tutores",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "estado",
                table: "Tutores",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "nombre",
                table: "Tutores",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "rol",
                table: "Tutores",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "apellido",
                table: "Docentes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Docentes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "estado",
                table: "Docentes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "nombre",
                table: "Docentes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "rol",
                table: "Docentes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "apellido",
                table: "Alumnos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Alumnos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "estado",
                table: "Alumnos",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "nombre",
                table: "Alumnos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "rol",
                table: "Alumnos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Alumnos",
                keyColumn: "dni",
                keyValue: 3001,
                columns: new[] { "apellido", "email", "estado", "nombre", "rol" },
                values: new object[] { "Lopez", "juan@student.com", true, "Juan", 1 });

            migrationBuilder.UpdateData(
                table: "Alumnos",
                keyColumn: "dni",
                keyValue: 3002,
                columns: new[] { "apellido", "email", "estado", "nombre", "rol" },
                values: new object[] { "Garcia", "maria@student.com", true, "María", 1 });

            migrationBuilder.UpdateData(
                table: "Docentes",
                keyColumn: "dni",
                keyValue: 1001,
                columns: new[] { "apellido", "email", "estado", "nombre", "rol" },
                values: new object[] { "Gomez", "carlos@gymail.com", true, "Carlos", 2 });

            migrationBuilder.UpdateData(
                table: "Docentes",
                keyColumn: "dni",
                keyValue: 1002,
                columns: new[] { "apellido", "email", "estado", "nombre", "rol" },
                values: new object[] { "Perez", "ana@school.com", true, "Ana", 2 });

            migrationBuilder.UpdateData(
                table: "Tutores",
                keyColumn: "dni",
                keyValue: 2001,
                columns: new[] { "apellido", "email", "estado", "nombre", "rol" },
                values: new object[] { "Ruiz", "laura@home.com", true, "Laura", 3 });
        }
    }
}

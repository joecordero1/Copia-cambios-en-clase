using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Final_Cordero_Raura.Migrations
{
    public partial class inicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pelicula",
                columns: table => new
                {
                    IdPelicula = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    anio = table.Column<int>(type: "int", nullable: false),
                    Poster = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pelicula", x => x.IdPelicula);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Cedula = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Cedula);
                });

            migrationBuilder.CreateTable(
                name: "Resena",
                columns: table => new
                {
                    IdResena = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Texto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeliculaIdPelicula = table.Column<int>(type: "int", nullable: true),
                    IdPelicula = table.Column<int>(type: "int", nullable: false),
                    UsuarioCedula = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resena", x => x.IdResena);
                    table.ForeignKey(
                        name: "FK_Resena_Pelicula_PeliculaIdPelicula",
                        column: x => x.PeliculaIdPelicula,
                        principalTable: "Pelicula",
                        principalColumn: "IdPelicula");
                    table.ForeignKey(
                        name: "FK_Resena_Usuario_UsuarioCedula",
                        column: x => x.UsuarioCedula,
                        principalTable: "Usuario",
                        principalColumn: "Cedula");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resena_PeliculaIdPelicula",
                table: "Resena",
                column: "PeliculaIdPelicula");

            migrationBuilder.CreateIndex(
                name: "IX_Resena_UsuarioCedula",
                table: "Resena",
                column: "UsuarioCedula");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resena");

            migrationBuilder.DropTable(
                name: "Pelicula");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Final_Cordero_Raura.Migrations
{
    public partial class AgregarSinopsis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resena_Pelicula_PeliculaIdPelicula",
                table: "Resena");

            migrationBuilder.DropIndex(
                name: "IX_Resena_PeliculaIdPelicula",
                table: "Resena");

            migrationBuilder.DropColumn(
                name: "PeliculaIdPelicula",
                table: "Resena");

            migrationBuilder.CreateIndex(
                name: "IX_Resena_IdPelicula",
                table: "Resena",
                column: "IdPelicula");

            migrationBuilder.AddForeignKey(
                name: "FK_Resena_Pelicula_IdPelicula",
                table: "Resena",
                column: "IdPelicula",
                principalTable: "Pelicula",
                principalColumn: "IdPelicula",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resena_Pelicula_IdPelicula",
                table: "Resena");

            migrationBuilder.DropIndex(
                name: "IX_Resena_IdPelicula",
                table: "Resena");

            migrationBuilder.AddColumn<int>(
                name: "PeliculaIdPelicula",
                table: "Resena",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resena_PeliculaIdPelicula",
                table: "Resena",
                column: "PeliculaIdPelicula");

            migrationBuilder.AddForeignKey(
                name: "FK_Resena_Pelicula_PeliculaIdPelicula",
                table: "Resena",
                column: "PeliculaIdPelicula",
                principalTable: "Pelicula",
                principalColumn: "IdPelicula");
        }
    }
}

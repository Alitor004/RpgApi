using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RpgApi.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoMuitosParaMuitos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Derrotas",
                table: "Personagens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Disputas",
                table: "Personagens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Vitorias",
                table: "Personagens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Habilidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dano = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habilidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonegemHabilidades",
                columns: table => new
                {
                    PersonagemId = table.Column<int>(type: "int", nullable: false),
                    HabilidadeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonegemHabilidades", x => new { x.PersonagemId, x.HabilidadeId });
                    table.ForeignKey(
                        name: "FK_PersonegemHabilidades_Habilidades_HabilidadeId",
                        column: x => x.HabilidadeId,
                        principalTable: "Habilidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonegemHabilidades_Personagens_PersonagemId",
                        column: x => x.PersonagemId,
                        principalTable: "Personagens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Habilidades",
                columns: new[] { "Id", "Dano", "Nome" },
                values: new object[,]
                {
                    { 1, 39, "Adormecer" },
                    { 2, 41, "Congelar" },
                    { 3, 37, "Hipnotizar" }
                });

            migrationBuilder.UpdateData(
                table: "Personagens",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Personagens",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Personagens",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Personagens",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Personagens",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Personagens",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Personagens",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 187, 145, 149, 96, 72, 71, 197, 143, 75, 51, 107, 223, 221, 216, 96, 179, 170, 54, 137, 171, 16, 113, 126, 99, 124, 24, 12, 62, 125, 75, 69, 21, 84, 115, 174, 201, 172, 153, 204, 59, 184, 86, 232, 179, 134, 170, 31, 52, 196, 232, 153, 158, 241, 166, 105, 16, 153, 231, 106, 17, 190, 96, 157, 173 }, new byte[] { 185, 29, 89, 229, 225, 148, 9, 50, 60, 118, 59, 184, 10, 126, 15, 150, 5, 30, 191, 6, 3, 2, 32, 159, 130, 188, 44, 151, 179, 106, 235, 248, 54, 181, 161, 2, 25, 80, 218, 213, 19, 108, 196, 15, 231, 148, 252, 145, 98, 227, 179, 153, 17, 186, 226, 245, 109, 71, 169, 211, 113, 66, 8, 65, 65, 2, 102, 73, 204, 175, 51, 172, 37, 34, 89, 212, 253, 77, 76, 243, 103, 189, 26, 61, 174, 162, 154, 62, 108, 215, 114, 33, 76, 228, 15, 41, 20, 241, 163, 34, 251, 130, 21, 135, 154, 219, 74, 187, 241, 199, 5, 210, 73, 209, 41, 41, 190, 126, 162, 213, 157, 100, 44, 196, 15, 175, 55, 207 } });

            migrationBuilder.InsertData(
                table: "PersonegemHabilidades",
                columns: new[] { "HabilidadeId", "PersonagemId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 3, 3 },
                    { 3, 4 },
                    { 1, 5 },
                    { 2, 6 },
                    { 3, 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonegemHabilidades_HabilidadeId",
                table: "PersonegemHabilidades",
                column: "HabilidadeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonegemHabilidades");

            migrationBuilder.DropTable(
                name: "Habilidades");

            migrationBuilder.DropColumn(
                name: "Derrotas",
                table: "Personagens");

            migrationBuilder.DropColumn(
                name: "Disputas",
                table: "Personagens");

            migrationBuilder.DropColumn(
                name: "Vitorias",
                table: "Personagens");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 46, 83, 216, 200, 235, 127, 221, 196, 70, 64, 237, 207, 247, 186, 232, 109, 8, 24, 23, 68, 3, 33, 82, 83, 130, 1, 121, 206, 3, 157, 45, 189, 190, 193, 52, 117, 72, 13, 211, 161, 221, 93, 241, 101, 128, 208, 229, 223, 0, 114, 47, 13, 253, 199, 118, 204, 89, 174, 228, 60, 226, 49, 141, 72 }, new byte[] { 158, 60, 203, 32, 15, 64, 202, 44, 117, 2, 222, 176, 104, 250, 221, 201, 125, 69, 196, 88, 39, 217, 33, 184, 31, 133, 11, 231, 69, 11, 76, 203, 67, 245, 112, 126, 224, 168, 63, 215, 247, 16, 173, 81, 153, 110, 7, 190, 201, 200, 90, 156, 53, 115, 56, 123, 32, 137, 27, 165, 155, 150, 230, 144, 208, 108, 10, 237, 101, 187, 238, 33, 197, 145, 78, 246, 179, 193, 208, 190, 31, 38, 201, 98, 91, 21, 43, 95, 67, 73, 192, 212, 119, 7, 67, 167, 17, 46, 154, 109, 73, 4, 6, 78, 54, 46, 118, 95, 27, 2, 233, 103, 113, 232, 159, 235, 73, 248, 239, 232, 35, 107, 60, 178, 92, 212, 160, 16 } });
        }
    }
}

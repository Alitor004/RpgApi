using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RpgApi.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoUmParaUm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonagemId",
                table: "Armas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Armas",
                keyColumn: "Id",
                keyValue: 1,
                column: "PersonagemId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Armas",
                keyColumn: "Id",
                keyValue: 2,
                column: "PersonagemId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Armas",
                keyColumn: "Id",
                keyValue: 3,
                column: "PersonagemId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Armas",
                keyColumn: "Id",
                keyValue: 4,
                column: "PersonagemId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Armas",
                keyColumn: "Id",
                keyValue: 5,
                column: "PersonagemId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Armas",
                keyColumn: "Id",
                keyValue: 6,
                column: "PersonagemId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Armas",
                keyColumn: "Id",
                keyValue: 7,
                column: "PersonagemId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 46, 83, 216, 200, 235, 127, 221, 196, 70, 64, 237, 207, 247, 186, 232, 109, 8, 24, 23, 68, 3, 33, 82, 83, 130, 1, 121, 206, 3, 157, 45, 189, 190, 193, 52, 117, 72, 13, 211, 161, 221, 93, 241, 101, 128, 208, 229, 223, 0, 114, 47, 13, 253, 199, 118, 204, 89, 174, 228, 60, 226, 49, 141, 72 }, new byte[] { 158, 60, 203, 32, 15, 64, 202, 44, 117, 2, 222, 176, 104, 250, 221, 201, 125, 69, 196, 88, 39, 217, 33, 184, 31, 133, 11, 231, 69, 11, 76, 203, 67, 245, 112, 126, 224, 168, 63, 215, 247, 16, 173, 81, 153, 110, 7, 190, 201, 200, 90, 156, 53, 115, 56, 123, 32, 137, 27, 165, 155, 150, 230, 144, 208, 108, 10, 237, 101, 187, 238, 33, 197, 145, 78, 246, 179, 193, 208, 190, 31, 38, 201, 98, 91, 21, 43, 95, 67, 73, 192, 212, 119, 7, 67, 167, 17, 46, 154, 109, 73, 4, 6, 78, 54, 46, 118, 95, 27, 2, 233, 103, 113, 232, 159, 235, 73, 248, 239, 232, 35, 107, 60, 178, 92, 212, 160, 16 } });

            migrationBuilder.CreateIndex(
                name: "IX_Armas_PersonagemId",
                table: "Armas",
                column: "PersonagemId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Armas_Personagens_PersonagemId",
                table: "Armas",
                column: "PersonagemId",
                principalTable: "Personagens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Armas_Personagens_PersonagemId",
                table: "Armas");

            migrationBuilder.DropIndex(
                name: "IX_Armas_PersonagemId",
                table: "Armas");

            migrationBuilder.DropColumn(
                name: "PersonagemId",
                table: "Armas");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 166, 180, 120, 38, 186, 171, 111, 50, 166, 215, 57, 230, 70, 210, 13, 252, 147, 198, 188, 52, 91, 170, 164, 210, 77, 92, 11, 113, 98, 244, 211, 21, 197, 177, 31, 160, 206, 72, 163, 91, 203, 121, 57, 19, 2, 209, 255, 181, 26, 117, 222, 108, 220, 79, 234, 243, 82, 128, 58, 118, 134, 111, 129, 75 }, new byte[] { 109, 251, 25, 88, 199, 0, 166, 210, 174, 45, 153, 13, 217, 50, 124, 155, 55, 36, 235, 203, 54, 71, 64, 191, 167, 105, 137, 186, 24, 36, 150, 121, 147, 204, 67, 175, 46, 206, 114, 170, 147, 220, 225, 87, 216, 64, 121, 147, 17, 254, 95, 86, 210, 23, 63, 101, 167, 88, 219, 167, 82, 181, 240, 254, 121, 59, 213, 164, 114, 25, 63, 155, 22, 122, 100, 209, 124, 232, 66, 12, 165, 92, 4, 172, 21, 47, 249, 161, 172, 235, 15, 65, 91, 150, 156, 123, 225, 129, 63, 160, 208, 120, 111, 85, 30, 253, 158, 31, 200, 207, 161, 233, 132, 182, 62, 42, 88, 103, 40, 205, 18, 75, 227, 107, 160, 122, 23, 78 } });
        }
    }
}

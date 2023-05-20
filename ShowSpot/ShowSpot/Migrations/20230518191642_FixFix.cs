using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShowSpot.Migrations
{
    /// <inheritdoc />
    public partial class FixFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4f32a6eb-46c2-4476-b77e-c1169f2868b0", "AQAAAAIAAYagAAAAEMFTqwjcOFZHO115q7knlAE3shplHK9wfLni9QIV1Nfojk/I37faZIkdpQs9sgePPw==", "fa3a82be-2de3-4495-92b7-874565a7c6e0" });

            migrationBuilder.InsertData(
                table: "Conteudos",
                columns: new[] { "Id", "AnoLancamento", "ImgUrl", "LinkTrailer", "Nome", "Rating", "Runtime", "Sinopse", "Tipo" },
                values: new object[,]
                {
                    { 1, "2001", "https://m.media-amazon.com/images/M/MV5BZTM2ZGJmNjQtN2UyOS00NjcxLWFjMDktMDE2NzMyNTZlZTBiXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_FMjpg_UX1000_.jpg", "https://www.youtube.com/watch?v=81mibtQWWBg", "American Psycho", "8.5/10", "1h20", "Em Nova York, em 1987, o belo jovem profissional Patrick Bateman tem uma segunda vida como um horrível assassino em série durante a noite. O elenco é formado pelo detetive, a noiva, a amante, o colega de trabalho e a secretária. Esta é uma comédia de humor seco que examina os elementos que transformam um homem em um monstro.", false },
                    { 2, "2014", "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcTz-JS71PMi4RnUlYf3rHkKwNISRcOV16lcwIqj7V88ypDNr150", "https://www.youtube.com/watch?v=zSWdZVtXT7E", "Interstellar", "7.5/10", "2h20", "As reservas naturais da Terra estão chegando ao fim e um grupo de astronautas recebe a missão de verificar possíveis planetas para receberem a população mundial, possibilitando a continuação da espécie. Cooper é chamado para liderar o grupo e aceita a missão sabendo que pode nunca mais ver os filhos. Ao lado de Brand, Jenkins e Doyle, ele seguirá em busca de um novo lar.", false },
                    { 3, "2017", "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcRqH5Zc-lpvXHfAHYaiMuqy5vSRparFrX_MyO46x4ZoWk-DiUa8", "https://www.youtube.com/watch?v=gCcx85zbxz4", "Blade Runner 2049", "8.5/10", "3h20", "Após descobrir um segredo que ameaça o que resta da sociedade, um novo policial parte em busca de Rick Deckard, que está desaparecido há 30 anos.", false },
                    { 4, "2008", "https://br.web.img3.acsta.net/pictures/14/03/31/19/28/462555.jpg", "https://www.youtube.com/watch?v=HhesaQXLuRY", "Breaking Bad", "10.5/10", "20 episodios, 5 temporadas", "O professor de química Walter White não acredita que sua vida possa piorar ainda mais. Quando descobre que tem câncer terminal, Walter decide arriscar tudo para ganhar dinheiro enquanto pode, transformando sua van em um laboratório de metanfetamina.", true }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Drama" },
                    { 2, "Comédia" },
                    { 3, "Mistério" },
                    { 4, "Aventura" }
                });

            migrationBuilder.InsertData(
                table: "ConteudoTags",
                columns: new[] { "Id", "ConteudoFK", "TagFK" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 2 },
                    { 4, 4, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ConteudoTags",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ConteudoTags",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ConteudoTags",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ConteudoTags",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Conteudos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Conteudos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Conteudos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Conteudos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f0e71ec4-7c58-43f3-b3b3-726aa75156a8", "AQAAAAIAAYagAAAAEEVwXnGaYp7fu1qlnjkL4e8ANupsQz/KK6q9RWsMElKiEEHSRsfp7AtTkk9ClMl6Sw==", "64f15c3c-1fb2-4e20-8a09-bc4198f4e7fd" });
        }
    }
}

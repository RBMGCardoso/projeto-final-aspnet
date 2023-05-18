using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShowSpot.Models;

namespace ShowSpot.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<IdentityUser>().HasData(
            new IdentityUser
            {
                Email = "rodrigtomas@gmail.com",
                NormalizedEmail = "RODRIGTOMAS@GMAIL.COM",
                UserName = "rodrigtomas@gmail.com",
                Id = "1",
                NormalizedUserName = "RODRIGTOMAS@GMAIL.COM",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "1234")
            }
        );

        builder.Entity<Conteudos>().HasKey(e => e.Id);
        // Criação dos conteudos iniciais na base de dados
        builder.Entity<Conteudos>().HasData(
            new Conteudos
            {
                Id = 1,
                Nome = "American Psycho",
                ImgUrl =
                    "https://m.media-amazon.com/images/M/MV5BZTM2ZGJmNjQtN2UyOS00NjcxLWFjMDktMDE2NzMyNTZlZTBiXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_FMjpg_UX1000_.jpg",
                Sinopse =
                    "Em Nova York, em 1987, o belo jovem profissional Patrick Bateman tem uma segunda vida como um horrível assassino em série durante a noite. O elenco é formado pelo detetive, a noiva, a amante, o colega de trabalho e a secretária. Esta é uma comédia de humor seco que examina os elementos que transformam um homem em um monstro.",
                Rating = "8.5/10",
                Tipo = false,
                Runtime = "1h20",
                AnoLancamento = "2001",
                LinkTrailer = "https://www.youtube.com/watch?v=81mibtQWWBg"
            },
            new Conteudos
            {
                Id = 2,
                Nome = "Interstellar",
                ImgUrl =
                    "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcTz-JS71PMi4RnUlYf3rHkKwNISRcOV16lcwIqj7V88ypDNr150",
                Sinopse =
                    "As reservas naturais da Terra estão chegando ao fim e um grupo de astronautas recebe a missão de verificar possíveis planetas para receberem a população mundial, possibilitando a continuação da espécie. Cooper é chamado para liderar o grupo e aceita a missão sabendo que pode nunca mais ver os filhos. Ao lado de Brand, Jenkins e Doyle, ele seguirá em busca de um novo lar.",
                Rating = "7.5/10",
                Tipo = false,
                Runtime = "2h20",
                AnoLancamento = "2014",
                LinkTrailer = "https://www.youtube.com/watch?v=zSWdZVtXT7E"
            },
            new Conteudos
            {
                Id = 3,
                Nome = "Blade Runner 2049",
                ImgUrl =
                    "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcRqH5Zc-lpvXHfAHYaiMuqy5vSRparFrX_MyO46x4ZoWk-DiUa8",
                Sinopse =
                    "Após descobrir um segredo que ameaça o que resta da sociedade, um novo policial parte em busca de Rick Deckard, que está desaparecido há 30 anos.",
                Rating = "8.5/10",
                Tipo = false,
                Runtime = "3h20",
                AnoLancamento = "2017",
                LinkTrailer = "https://www.youtube.com/watch?v=gCcx85zbxz4"
            },
            new Conteudos
            {
                Id = 4,
                Nome = "Breaking Bad",
                ImgUrl =
                    "https://br.web.img3.acsta.net/pictures/14/03/31/19/28/462555.jpg",
                Sinopse =
                    "O professor de química Walter White não acredita que sua vida possa piorar ainda mais. Quando descobre que tem câncer terminal, Walter decide arriscar tudo para ganhar dinheiro enquanto pode, transformando sua van em um laboratório de metanfetamina.",
                Rating = "10.5/10",
                Tipo = true,
                Runtime = "20 episodios, 5 temporadas",
                AnoLancamento = "2008",
                LinkTrailer = "https://www.youtube.com/watch?v=HhesaQXLuRY"
            }
        );

        
        // Criação das tags iniciais na base de dados
        builder.Entity<Tags>().HasData(
            new Tags
            {
                Id = 1,
                Nome = "Drama"
            },
            new Tags
            {
                Id = 2,
                Nome = "Comédia"
            },
            new Tags
            {
                Id = 3,
                Nome = "Mistério"
            },
            new Tags
            {
                Id = 4,
                Nome = "Aventura"
            }
        );

        builder.Entity<ConteudoTags>()
            .Property(c => c.Id)
            .ValueGeneratedOnAdd();
        
        builder.Entity<ConteudoTags>().HasData(
            new ConteudoTags
            {
                Id = 1,
                ConteudoFK = 1,
                TagFK = 1
            },
            new ConteudoTags
            {
                Id = 2,
                ConteudoFK = 2,
                TagFK = 1
            },
            new ConteudoTags
            {
                Id = 3,
                ConteudoFK = 3,
                TagFK = 2
            },
            new ConteudoTags
            {
                Id = 4,
                ConteudoFK = 4,
                TagFK = 3
            }
        );
    }

    public DbSet<Conteudos> Conteudos { get; set; }
    public DbSet<ConteudoTags> ConteudoTags { get; set; }
    public DbSet<Favoritos> Favoritos { get; set; }
    public DbSet<Tags> Tags { get; set; }
    public DbSet<WatchLaters> WatchLaters { get; set; }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShowSpot.Data;

#nullable disable

namespace ShowSpot.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "ad6bb408-9ae1-4dbc-adfc-34f1155d4b93",
                            Email = "rodrigtomas@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "RODRIGTOMAS@GMAIL.COM",
                            NormalizedUserName = "RODRIGTOMAS@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEARWuCOyv6nTWB2y+ZBtu4x66XAl/1lyLoikdanl/ygkzUypUv+jIwGyCFS3hh+lYw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "f83d035d-e74f-40c8-a953-5aacc98439f6",
                            TwoFactorEnabled = false,
                            UserName = "rodrigtomas@gmail.com"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ShowSpot.Models.ConteudoTags", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ConteudoFK")
                        .HasColumnType("int");

                    b.Property<int>("TagFK")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConteudoFK");

                    b.HasIndex("TagFK");

                    b.ToTable("ConteudoTags");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConteudoFK = 1,
                            TagFK = 1
                        },
                        new
                        {
                            Id = 2,
                            ConteudoFK = 2,
                            TagFK = 1
                        },
                        new
                        {
                            Id = 3,
                            ConteudoFK = 3,
                            TagFK = 2
                        },
                        new
                        {
                            Id = 4,
                            ConteudoFK = 4,
                            TagFK = 3
                        });
                });

            modelBuilder.Entity("ShowSpot.Models.Conteudos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AnoLancamento")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ImgUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LinkTrailer")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Rating")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Runtime")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Sinopse")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Tipo")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Conteudos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AnoLancamento = "2001",
                            ImgUrl = "https://m.media-amazon.com/images/M/MV5BZTM2ZGJmNjQtN2UyOS00NjcxLWFjMDktMDE2NzMyNTZlZTBiXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_FMjpg_UX1000_.jpg",
                            LinkTrailer = "https://www.youtube.com/watch?v=81mibtQWWBg",
                            Nome = "American Psycho",
                            Rating = "8.5/10",
                            Runtime = "1h20",
                            Sinopse = "Em Nova York, em 1987, o belo jovem profissional Patrick Bateman tem uma segunda vida como um horrível assassino em série durante a noite. O elenco é formado pelo detetive, a noiva, a amante, o colega de trabalho e a secretária. Esta é uma comédia de humor seco que examina os elementos que transformam um homem em um monstro.",
                            Tipo = false
                        },
                        new
                        {
                            Id = 2,
                            AnoLancamento = "2014",
                            ImgUrl = "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcTz-JS71PMi4RnUlYf3rHkKwNISRcOV16lcwIqj7V88ypDNr150",
                            LinkTrailer = "https://www.youtube.com/watch?v=zSWdZVtXT7E",
                            Nome = "Interstellar",
                            Rating = "7.5/10",
                            Runtime = "2h20",
                            Sinopse = "As reservas naturais da Terra estão chegando ao fim e um grupo de astronautas recebe a missão de verificar possíveis planetas para receberem a população mundial, possibilitando a continuação da espécie. Cooper é chamado para liderar o grupo e aceita a missão sabendo que pode nunca mais ver os filhos. Ao lado de Brand, Jenkins e Doyle, ele seguirá em busca de um novo lar.",
                            Tipo = false
                        },
                        new
                        {
                            Id = 3,
                            AnoLancamento = "2017",
                            ImgUrl = "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcRqH5Zc-lpvXHfAHYaiMuqy5vSRparFrX_MyO46x4ZoWk-DiUa8",
                            LinkTrailer = "https://www.youtube.com/watch?v=gCcx85zbxz4",
                            Nome = "Blade Runner 2049",
                            Rating = "8.5/10",
                            Runtime = "3h20",
                            Sinopse = "Após descobrir um segredo que ameaça o que resta da sociedade, um novo policial parte em busca de Rick Deckard, que está desaparecido há 30 anos.",
                            Tipo = false
                        },
                        new
                        {
                            Id = 4,
                            AnoLancamento = "2008",
                            ImgUrl = "https://br.web.img3.acsta.net/pictures/14/03/31/19/28/462555.jpg",
                            LinkTrailer = "https://www.youtube.com/watch?v=HhesaQXLuRY",
                            Nome = "Breaking Bad",
                            Rating = "10.5/10",
                            Runtime = "20 episodios, 5 temporadas",
                            Sinopse = "O professor de química Walter White não acredita que sua vida possa piorar ainda mais. Quando descobre que tem câncer terminal, Walter decide arriscar tudo para ganhar dinheiro enquanto pode, transformando sua van em um laboratório de metanfetamina.",
                            Tipo = true
                        });
                });

            modelBuilder.Entity("ShowSpot.Models.Favoritos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ConteudosFK")
                        .HasColumnType("int");

                    b.Property<string>("UserFK")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("ConteudosFK");

                    b.HasIndex("UserFK");

                    b.ToTable("Favoritos");
                });

            modelBuilder.Entity("ShowSpot.Models.Tags", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nome = "Drama"
                        },
                        new
                        {
                            Id = 2,
                            Nome = "Comédia"
                        },
                        new
                        {
                            Id = 3,
                            Nome = "Mistério"
                        },
                        new
                        {
                            Id = 4,
                            Nome = "Aventura"
                        });
                });

            modelBuilder.Entity("ShowSpot.Models.WatchLaters", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ConteudosFK")
                        .HasColumnType("int");

                    b.Property<string>("UtilizadorFK")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("ConteudosFK");

                    b.HasIndex("UtilizadorFK");

                    b.ToTable("WatchLaters");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ShowSpot.Models.ConteudoTags", b =>
                {
                    b.HasOne("ShowSpot.Models.Conteudos", "Conteudo")
                        .WithMany()
                        .HasForeignKey("ConteudoFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShowSpot.Models.Tags", "Tag")
                        .WithMany()
                        .HasForeignKey("TagFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Conteudo");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("ShowSpot.Models.Favoritos", b =>
                {
                    b.HasOne("ShowSpot.Models.Conteudos", "Conteudo")
                        .WithMany()
                        .HasForeignKey("ConteudosFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Conteudo");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShowSpot.Models.WatchLaters", b =>
                {
                    b.HasOne("ShowSpot.Models.Conteudos", "Conteudo")
                        .WithMany()
                        .HasForeignKey("ConteudosFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UtilizadorFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Conteudo");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}

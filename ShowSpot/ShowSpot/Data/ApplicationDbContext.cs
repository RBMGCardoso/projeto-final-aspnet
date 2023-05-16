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
                NormalizedUserName = "RODRIGTOMAS@GMAIL.COM",
                Id = Guid.NewGuid().ToString(),
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "1234")
            }
        ) ;
    }

    public DbSet<Conteudos> Conteudos { get; set; }
    public DbSet<ConteudoTags> ConteudoTags { get; set; }
    public DbSet<Recomendados> Recomendados { get; set; }
    public DbSet<Tags> Tags { get; set; }
    public DbSet<Utilizadores> Utilizadores { get; set; }
    public DbSet<WatchLaters> WatchLaters { get; set; }
}

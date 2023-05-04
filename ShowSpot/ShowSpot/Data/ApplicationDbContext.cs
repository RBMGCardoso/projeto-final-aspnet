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
    
    public DbSet<Conteudos> Conteudos { get; set; }
    public DbSet<ConteudoTags> ConteudoTags { get; set; }
    public DbSet<Recomendados> Recomendados { get; set; }
    public DbSet<Tags> Tags { get; set; }
    public DbSet<Utilizadores> Utilizadores { get; set; }
    public DbSet<WatchLaters> WatchLaters { get; set; }
}

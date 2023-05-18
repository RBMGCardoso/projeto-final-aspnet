using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ShowSpot.Models;

public class Favoritos
{
    public int Id { get; set; }
    
    [ForeignKey(nameof(User))]
    public string UserFK { get; set; }
    public IdentityUser User { get; set; }
    
    [ForeignKey(nameof(Conteudo))]
    public int ConteudosFK { get; set; }
    public Conteudos Conteudo { get; set; }
}
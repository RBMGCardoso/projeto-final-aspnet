using System.ComponentModel.DataAnnotations.Schema;

namespace ShowSpot.Models;

public class Recomendados
{
    public int Id { get; set; }
    
    [ForeignKey(nameof(Utilizador))]
    public int UtilizadorFK { get; set; }
    public Utilizadores Utilizador { get; set; }
    
    [ForeignKey(nameof(Conteudo))]
    public int ConteudosFK { get; set; }
    public Conteudos Conteudo { get; set; }
}
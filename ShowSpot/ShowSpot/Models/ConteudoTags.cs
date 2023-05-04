using System.ComponentModel.DataAnnotations.Schema;

namespace ShowSpot.Models;

public class ConteudoTags
{
    public int Id { get; set; }
    
    [ForeignKey(nameof(Conteudo))]
    public int ConteudoFK { get; set; }
    public Conteudos Conteudo { get; set; }
    
    [ForeignKey(nameof(Tag))]
    public int TagFK { get; set; }
    public Tags Tag { get; set; }
}
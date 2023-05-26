using System.ComponentModel.DataAnnotations.Schema;

namespace ShowSpot.Models;

/*
 * Esta classe representa a relação muitos para muitos entre os conteúdos e as tags. 
 */
public class ConteudoTags
{
    /*
     * Id para cada entrada na tabela ConteudoTags
     */
    public int Id { get; set; }
    
    /*
     * Chave forasteira para cada conteúdo na BD
     */
    [ForeignKey(nameof(Conteudo))]
    public int ConteudoFK { get; set; }
    public Conteudos Conteudo { get; set; }
    
    /*
     * Chave forasteira para cada tag na BD
     */
    [ForeignKey(nameof(Tag))]
    public int TagFK { get; set; }
    public Tags Tag { get; set; }
}
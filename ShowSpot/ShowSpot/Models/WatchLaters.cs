using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ShowSpot.Models;

public class WatchLaters
{
    /*
     * Id de cada entrada na tabela da BD
     */
    public int Id { get; set; }
    
    /*
     * Chave forasteira para o utilizador
     */
    [ForeignKey(nameof(User))]
    public string UtilizadorFK { get; set; }
    public IdentityUser User { get; set; }
    
    /*
     * Chave forasteira para o conteúdo marcado para ver mais tarde pelo utilizador
     */
    [ForeignKey(nameof(Conteudo))]
    public int ConteudosFK { get; set; }
    public Conteudos Conteudo { get; set; }
}
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ShowSpot.Models;

public class Favoritos
{
    /*
     * Id de cada entrada na tabela de favoritos.
     */
    public int Id { get; set; }
    
    /*
     * Chave forasteira para um utilizador
     */
    [ForeignKey(nameof(User))]
    public string UserFK { get; set; }
    public IdentityUser User { get; set; }
    
    /*
     * Chave forasteira para o conteúdo marcado como favorito pelo utilizador
     */
    [ForeignKey(nameof(Conteudo))]
    public int ConteudosFK { get; set; }
    public Conteudos Conteudo { get; set; }
}
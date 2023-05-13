using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ShowSpot.Models;

public class Conteudos
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string ImgUrl { get; set; }
    public string Sinopse { get; set; }

    [RegularExpression(@"^(10|[0-9])\/10$", ErrorMessage = "Rating should be in the format 'x/10', where x is a number between 0 and 10.")]
    public string Rating { get; set; }
    public bool Tipo { get; set; }
    public string Runtime { get; set; }
}
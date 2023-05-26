using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ShowSpot.Models;

public class Conteudos
{
    /*
     * Id de cada conteúdo
     */
    public int Id { get; set; }

    /*
     * Nome de cada conteúdo
     */
    public string Nome { get; set; }

    /*
     * URL para a imagem de cada conteúdo
     */
    public string ImgUrl { get; set; }

    /*
     * Sinopse para cada conteúdo
     */
    public string Sinopse { get; set; }

    /*
     * Rating para cada conteúdo. O regex obriga a utilização do formato x/10 sendo x um número entre 0 e 10. 
    */
    [RegularExpression(@"^(10|[0-9])\/10$", ErrorMessage = "O rating deve de ter o formato x/10.")]
    public string Rating { get; set; }

    /*
     * Tipo de cada conteúdo, se for 1 é uma série se for 0 é um filme.
     */
    public bool Tipo { get; set; }

    /*
     * Runtime para cada conteúdo. Pode ser o número de horas se for um filme ou o número de episódios e temporadas se for uma série.
     */
    public string Runtime { get; set; }

    /*
     * Ano de lançamento de cada conteúdo. O regex obriga a utilização do formato xxxx sendo xxxx um ano válido.
     */
    [RegularExpression(@"^\d{4}$", ErrorMessage = "Por favor introduza um ano válido")]
    public string AnoLancamento { get; set; }

    /*
     * Link para o youtube. O link tem de ser /embed/ para permitir a sua reprodução na aplicação.
     */
    public string LinkTrailer { get; set; }
}
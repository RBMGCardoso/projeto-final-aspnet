using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ShowSpot.Models;

public class Conteudos
{
    /*
     * Id de cada conte�do
     */
    public int Id { get; set; }

    /*
     * Nome de cada conte�do
     */
    public string Nome { get; set; }

    /*
     * URL para a imagem de cada conte�do
     */
    public string ImgUrl { get; set; }

    /*
     * Sinopse para cada conte�do
     */
    public string Sinopse { get; set; }

    /*
     * Rating para cada conte�do. O regex obriga a utiliza��o do formato x/10 sendo x um n�mero entre 0 e 10. 
    */
    [RegularExpression(@"^(10|[0-9])\/10$", ErrorMessage = "O rating deve de ter o formato x/10.")]
    public string Rating { get; set; }

    /*
     * Tipo de cada conte�do, se for 1 � uma s�rie se for 0 � um filme.
     */
    public bool Tipo { get; set; }

    /*
     * Runtime para cada conte�do. Pode ser o n�mero de horas se for um filme ou o n�mero de epis�dios e temporadas se for uma s�rie.
     */
    public string Runtime { get; set; }

    /*
     * Ano de lan�amento de cada conte�do. O regex obriga a utiliza��o do formato xxxx sendo xxxx um ano v�lido.
     */
    [RegularExpression(@"^\d{4}$", ErrorMessage = "Por favor introduza um ano v�lido")]
    public string AnoLancamento { get; set; }

    /*
     * Link para o youtube. O link tem de ser /embed/ para permitir a sua reprodu��o na aplica��o.
     */
    public string LinkTrailer { get; set; }
}
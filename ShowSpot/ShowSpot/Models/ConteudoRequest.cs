namespace ShowSpot.Models
{
    public class ConteudoRequest
    {
        public string Nome { get; set; }
        public string ImgUrl { get; set; }
        public string Sinopse { get; set; }
        public string Rating { get; set; }
        public bool Tipo { get; set; }
        public string Runtime { get; set; }
        public string AnoLancamento { get; set; }
        public string LinkTrailer { get; set; }
    }
}

namespace ShowSpot.Models
{
    /*
     * Classe auxiliar utilizada para processar o pedido de alteração de password de um utilizador.
     */
    public class PasswordChangeRequest
    {
        /*
         * Atributo de username do utilizador que está a tentar alterar a palavra-passe
         */
        public string Username { get; set; }

        /*
         * Atributo que representa a password nova do utilizador
         */
        public string Password { get; set; }
    }
}

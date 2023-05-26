namespace ShowSpot.Models
{
    /*
     * Classe auxiliar, é utilizada apenas para processar o pedido de login no servidor. Utilizada também no registo do utilizador no servidor.
     */
    public class LogInRequest
    {
        /*
         * Username do utilizador que efetuou o pedido de login
         */
        public string UserName { get; set; }

        /*
         * Email do utilizador que efetuou o pedido de login
         */
        public string Email { get; set;}

        /*
         * Password do utilizador que efetuou o pedido de login
         */
        public string Password { get; set;}
    }
}

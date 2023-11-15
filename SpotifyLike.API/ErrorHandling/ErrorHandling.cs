namespace SpotifyLike.API.ErrorHandling
{
    public class ErrorHandling
    {
        public List<ErrorMessage> Messages { get; set; } = new List<ErrorMessage>();
        public String ErrorDescription = "Aconteceram erros ao processar sua requisição";
    }

    public class ErrorMessage
    {
        public string Message { get; set; }
        public string ErrorName { get; set; }
    }
}

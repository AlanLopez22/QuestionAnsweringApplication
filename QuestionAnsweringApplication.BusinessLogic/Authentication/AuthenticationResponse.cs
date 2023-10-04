namespace QuestionAnsweringApplication.BusinessLogic.Authentication
{
    public sealed class AuthenticationResponse
    {
        public string? AccessToken { get; set; }
        public string? Error { get; set;}
    }
}

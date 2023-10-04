using MediatR;

namespace QuestionAnsweringApplication.BusinessLogic.Authentication
{
    public sealed class UserCommand : IRequest<AuthenticationResponse>
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}

using MediatR;

namespace QuestionAnsweringApplication.BusinessLogic.Questions
{
    public sealed class CreateQuestionCommand : IRequest<QuestionResponse>
    {
        public Guid UserId { get; set; }
        public string? Question { get; set; }
        public IEnumerable<string> Tags { get; set; } = new List<string>();
    }
}

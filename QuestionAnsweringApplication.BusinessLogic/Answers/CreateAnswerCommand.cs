using MediatR;

namespace QuestionAnsweringApplication.BusinessLogic.Answers
{
    public sealed class CreateAnswerCommand : IRequest<AnswerResponse>
    {
        public Guid UserId { get; set; }
        public Guid QuestionId { get; set; }
        public string? Answer { get; set; }
        public IEnumerable<string> Tags { get; set; } = new List<string>();
    }
}

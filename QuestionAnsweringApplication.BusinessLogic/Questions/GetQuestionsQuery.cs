using MediatR;

namespace QuestionAnsweringApplication.BusinessLogic.Questions
{
    public class GetQuestionsQuery : IRequest<List<QuestionResponse>>
    {
        public IEnumerable<string> Tags { get; set; } = new List<string>();

        public GetQuestionsQuery(IEnumerable<string> tags)
        {
            Tags = tags;
        }
    }
}

using MediatR;

namespace QuestionAnsweringApplication.BusinessLogic.Questions
{
    public class GetQuestionDetailsQuery : IRequest<QuestionDetail>
    {
        public Guid QuestionId { get; set; }

        public GetQuestionDetailsQuery(Guid questionId)
        {
            QuestionId = questionId;
        }
    }
}

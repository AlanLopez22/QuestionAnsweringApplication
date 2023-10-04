namespace QuestionAnsweringApplication.Domain
{
    public class AnswerTag : Entity
    {
        public Guid AnswerId { get; set; }
        public Guid TagId { get; set; }
        public Answer? Answer { get; set; }
        public Tag? Tag { get; set; }
    }
}

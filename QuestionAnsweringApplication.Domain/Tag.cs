namespace QuestionAnsweringApplication.Domain
{
    public class Tag : Entity
    {
        public string? Name { get; set; }
        public ICollection<AnswerTag> Answers { get; set; } = new HashSet<AnswerTag>();
        public ICollection<QuestionTag> Questions { get; set; } = new HashSet<QuestionTag>();
    }
}

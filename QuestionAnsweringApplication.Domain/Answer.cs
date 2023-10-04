namespace QuestionAnsweringApplication.Domain
{
    public class Answer : Entity
    {
        public string? AnswerText { get; set; }
        public Guid QuestionId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Question? Question { get; set; }
        public User? User { get; set; }
        public ICollection<AnswerTag> Tags { get; set; } = new HashSet<AnswerTag>();
    }
}

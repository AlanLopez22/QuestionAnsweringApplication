namespace QuestionAnsweringApplication.Domain
{
    public class Question : Entity
    {
        public string? QuestionText { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public User? User { get; set; }
        public ICollection<QuestionTag> Tags { get; set; } = new HashSet<QuestionTag>();
        public ICollection<Answer> Answers { get; set; } = new HashSet<Answer>();
    }
}

namespace QuestionAnsweringApplication.BusinessLogic.Questions
{
    public class QuestionDetail
    {
        public Guid Id { get; set; }
        public string? Question { get; set; }
        public IEnumerable<string> Answers { get; set; } = new List<string>();
        public IEnumerable<string> Tags { get; set; } = new List<string>();
    }
}

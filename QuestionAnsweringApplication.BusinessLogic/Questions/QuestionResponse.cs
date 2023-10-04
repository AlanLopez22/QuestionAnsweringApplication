namespace QuestionAnsweringApplication.BusinessLogic.Questions
{
    public sealed class QuestionResponse
    {
        public Guid Id { get; set; }
        public string? Question { get; set; }
        public IEnumerable<string> Tags { get; set; } = new List<string>();
    }
}

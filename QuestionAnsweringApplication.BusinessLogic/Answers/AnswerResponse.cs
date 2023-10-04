namespace QuestionAnsweringApplication.BusinessLogic.Answers
{
    public sealed class AnswerResponse
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public string? Question { get; set; }
        public string? Answer { get; set; }
        public IEnumerable<string> Tags { get; set; } = new List<string>();
    }
}

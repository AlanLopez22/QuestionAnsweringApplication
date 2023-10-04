namespace QuestionAnsweringApplication.BusinessLogic.BulkImport
{
    public sealed class BulkImportResponse
    {
        public int TotalQuestionsAdded { get; set; } = 0;
        public int TotalAnswersAdded { get; set; } = 0;
        public List<string> Errors { get; set; } = new List<string>();
    }
}

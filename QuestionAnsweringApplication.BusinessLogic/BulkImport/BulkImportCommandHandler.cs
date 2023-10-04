using CsvHelper;
using MediatR;
using QuestionAnsweringApplication.BusinessLogic.Answers;
using QuestionAnsweringApplication.BusinessLogic.Questions;
using System.Globalization;

namespace QuestionAnsweringApplication.BusinessLogic.BulkImport
{
    public sealed class BulkImportCommandHandler : IRequestHandler<BulkImportCommand, BulkImportResponse>
    {
        private readonly IMediator _mediator;

        public BulkImportCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<BulkImportResponse> Handle(BulkImportCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.File == null)
            {
                throw new ArgumentNullException(nameof(request.File));
            }

            using var reader = new StreamReader(request.File.OpenReadStream());
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecordsAsync<BulkImportModel>(cancellationToken);
            var response = await ProcessRecords(request, records);

            return response;
        }

        private async Task<BulkImportResponse> ProcessRecords(BulkImportCommand request, IAsyncEnumerable<BulkImportModel> records)
        {
            BulkImportResponse response = new();
            await foreach (var record in records)
            {
                if (string.IsNullOrEmpty(record.Question) || string.IsNullOrEmpty(record.Answer))
                {
                    continue;
                }

                List<string> tags = new();

                if (!string.IsNullOrEmpty(record.Tags))
                {
                    tags.AddRange(record.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries));
                }

                var questionResponse = await AddQuestion(request.UserId, record, tags, response);
                await AddAnswer(request.UserId, record, tags, questionResponse.Id, response);
            }

            return response;
        }

        private async Task<QuestionResponse> AddQuestion(Guid userId, BulkImportModel record, IEnumerable<string> tags, BulkImportResponse response)
        {
            var questionResponse = await _mediator.Send(new CreateQuestionCommand
            {
                Question = record.Question,
                Tags = tags,
                UserId = userId,
            });

            if (questionResponse != null && questionResponse.Id != Guid.Empty)
            {
                response.TotalQuestionsAdded++;
            }

            return questionResponse!;
        }

        private async Task AddAnswer(Guid userId, BulkImportModel record, IEnumerable<string> tags, Guid questionId, BulkImportResponse response)
        {
            var answerResponse = await _mediator.Send(new CreateAnswerCommand
            {
                QuestionId = questionId,
                Answer = record.Answer,
                Tags = tags,
                UserId = userId,
            });

            if (answerResponse != null && answerResponse.Id != Guid.Empty)
            {
                response.TotalAnswersAdded++;
            }
        }
    }
}

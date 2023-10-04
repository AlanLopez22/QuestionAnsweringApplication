using MediatR;
using Microsoft.AspNetCore.Http;

namespace QuestionAnsweringApplication.BusinessLogic.BulkImport
{
    public sealed class BulkImportCommand : IRequest<BulkImportResponse>
    {
        public Guid UserId { get; set; }
        public IFormFile? File { get; set; }
    }
}

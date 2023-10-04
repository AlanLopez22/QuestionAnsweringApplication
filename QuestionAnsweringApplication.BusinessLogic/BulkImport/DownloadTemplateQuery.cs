using MediatR;

namespace QuestionAnsweringApplication.BusinessLogic.BulkImport
{
    public class DownloadTemplateQuery : IRequest<byte[]>
    {
    }
}

using CsvHelper;
using MediatR;
using System.Globalization;

namespace QuestionAnsweringApplication.BusinessLogic.BulkImport
{
    public class DownloadTemplateQueryHandler : IRequestHandler<DownloadTemplateQuery, byte[]>
    {
        public async Task<byte[]> Handle(DownloadTemplateQuery request, CancellationToken cancellationToken)
        {
            using (var ms = new MemoryStream())
            {
                using (var writer = new StreamWriter(ms))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    await csv.WriteRecordsAsync(new List<BulkImportModel>()
                    {
                        new BulkImportModel
                        {
                            Question = "",
                            Answer = "",
                            Tags = ""
                        }
                    }, cancellationToken);
                }
                var bytes = ms.ToArray();
                return bytes;
            }
        }
    }
}

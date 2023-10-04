using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionAnsweringApplication.BusinessLogic.BulkImport;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace QuestionAnsweringApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BulkImportController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BulkImportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file)
        {
            BulkImportCommand command = new()
            {
                UserId = this.User.Identity!.GetUserId(),
                File = file
            };
            var response = await _mediator.Send(command);

            if (response == null)
            {
                return BadRequest();
            }

            return Ok(response);
        }

        [Route("download-template")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> DownloadTemplate()
        {
            DownloadTemplateQuery query = new();
            var response = await _mediator.Send(query);

            if (response == null || response.Length == 0)
            {
                return BadRequest();
            }

            return File(response, "application/octet-stream", "file-example.csv");
        }
    }
}

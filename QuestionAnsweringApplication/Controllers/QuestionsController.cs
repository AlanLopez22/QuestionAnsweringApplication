using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionAnsweringApplication.BusinessLogic.Questions;

namespace QuestionAnsweringApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuestionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateQuestionCommand command)
        {
            command.UserId = this.User.Identity!.GetUserId();
            var response = await _mediator.Send(command);

            if (response == null)
            {
                return BadRequest();
            }

            return Ok(response);
        }

        [Route("/{id}")]
        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _mediator.Send(new GetQuestionDetailsQuery(id));

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [Route("/search-by-tags")]
        [HttpPost]
        public async Task<IActionResult> Get(List<string> tags)
        {
            var response = await _mediator.Send(new GetQuestionsQuery(tags));

            if (response == null)
            {
                return BadRequest();
            }

            return Ok(response);
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionAnsweringApplication.BusinessLogic.Authentication;

namespace QuestionAnsweringApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserCommand command)
        {
            var response = await _mediator.Send(command);
            
            if (response == null)
            {
                return BadRequest();
            }

            if (!string.IsNullOrEmpty(response.Error))
            {
                return BadRequest(response.Error);
            }

            if (string.IsNullOrEmpty(response.AccessToken))
            {
                return BadRequest("Could not generate access token");
            }

            return Ok(response.AccessToken);
        }
    }
}

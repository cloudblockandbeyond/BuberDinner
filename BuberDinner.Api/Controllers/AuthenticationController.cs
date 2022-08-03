using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using MediatR;
using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Application.Authentication.Common;
using MapsterMapper;

namespace BuberDinner.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AuthenticationController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AuthenticationController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var command = _mapper.Map<RegisterCommand>(request);
            ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

            return authResult.Match(
                authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                error => Problem(error)
            );
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var query = _mapper.Map<LoginQuery>(request);
            ErrorOr<AuthenticationResult> authResult = await _mediator.Send(query);

            return authResult.Match(
                authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                error => Problem(error)
            );
        }
    }
}
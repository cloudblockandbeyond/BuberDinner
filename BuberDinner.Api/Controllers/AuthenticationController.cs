using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;

namespace BuberDinner.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AuthenticationController : ApiController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            ErrorOr<AuthenticationResult> authResult = _authenticationService.Register(request.FirstName
            , request.LastName, request.Email, request.Password);

            return authResult.Match(
                authResult => Ok(MapAuthResult(authResult)),
                error => Problem(error));
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            ErrorOr<AuthenticationResult> authResult = _authenticationService.Login(request.Email, request.Password);

            return authResult.Match(
                authResult => Ok(MapAuthResult(authResult)),
                error => Problem(error));
        }

        public static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
        {
            var response = new AuthenticationResponse (
                authResult.user.Id,
                authResult.user.FirstName,
                authResult.user.LastName,
                authResult.user.Email,
                authResult.Token
            );

            return response;
        }
    }
}
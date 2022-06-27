using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            var result = _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);
            
            var response = new AuthenticationResponse (
                result.Id, 
                result.FirstName, 
                result.LastName, 
                result.Email, 
                result.Token
            );
            
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var result = _authenticationService.Login(request.Email, request.Password);
            
            var response = new AuthenticationResponse (
                result.Id, 
                result.FirstName, 
                result.LastName, 
                result.Email, 
                result.Token
            );
            
            return Ok(response);
        }
    }
}
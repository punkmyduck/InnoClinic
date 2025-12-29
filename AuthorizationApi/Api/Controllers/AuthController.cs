using AuthorizationApi.Api.DTOs;
using AuthorizationApi.Application.Abstractions;
using AuthorizationApi.Application.Commands;
using AuthorizationApi.Application.Results;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationApi.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ICommandHandler<RegisterCommand, RegisterCommandResult> _registerHandler;
        public AuthController(
            ICommandHandler<RegisterCommand, RegisterCommandResult> registerHandler
            )
        {
            _registerHandler = registerHandler;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request, CancellationToken cancellationToken)
        {
            var command = new RegisterCommand(
                    request.Email,
                    request.Password,
                    request.PhoneNumber
                );

            var result = await _registerHandler.HandleAsync(command, cancellationToken);

            return CreatedAtAction(nameof(Register), new RegisterResponseDto(result.AccountId, result.IsEmailVerificationRequired));
        }

        [HttpGet("signin")]
        public async Task<IActionResult> SignIn()
        {
            throw new NotImplementedException();
        }
    }
}

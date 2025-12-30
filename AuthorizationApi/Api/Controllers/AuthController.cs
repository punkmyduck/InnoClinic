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
        private readonly ICommandHandler<SignInCommand, SignInCommandResult> _signInHandler;
        public AuthController(
            ICommandHandler<RegisterCommand, RegisterCommandResult> registerHandler,
            ICommandHandler<SignInCommand, SignInCommandResult> signInHandler
            )
        {
            _registerHandler = registerHandler;
            _signInHandler = signInHandler;
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

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequestDto request, CancellationToken cancellationToken)
        {
            var command = new SignInCommand(
                request.Email,
                request.Password);
            
            var result = await _signInHandler.HandleAsync(command, cancellationToken);

            return Ok(new SignInResponseDto(result.IsSuccessful, result.JwtToken, result.ExpiresAt, result.RefreshToken));
        }
    }
}

using AuthorizationApi.Api.DTOs;
using AuthorizationApi.Application.Abstractions;
using AuthorizationApi.Application.Commands;
using AuthorizationApi.Application.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationApi.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ICommandHandler<RegisterCommand, RegisterCommandResult> _registerHandler;
        private readonly ICommandHandler<SignInCommand, SignInCommandResult> _signInHandler;
        private readonly ICommandHandler<RefreshCommand, RefreshCommandResult> _refreshHandler;
        public AuthController(
            ICommandHandler<RegisterCommand, RegisterCommandResult> registerHandler,
            ICommandHandler<SignInCommand, SignInCommandResult> signInHandler,
            ICommandHandler<RefreshCommand, RefreshCommandResult> refreshHandler
            )
        {
            _registerHandler = registerHandler;
            _signInHandler = signInHandler;
            _refreshHandler = refreshHandler;
        }

        /// <summary>
        /// Registers a new user account using the provided registration details.
        /// </summary>
        /// <param name="request">The registration information for the new user, including email, password, and phone number. Cannot be null.</param>
        /// <param name="cancellationToken">A token that can be used to cancel the registration operation.</param>
        /// <returns>A response indicating the result of the registration operation. Returns a 201 Created response with account
        /// details if successful.</returns>
        [HttpPost("register")]
        [AllowAnonymous]
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

        /// <summary>
        /// Authenticates a user with the provided credentials and returns a JWT token if authentication is successful.
        /// </summary>
        /// <param name="request">The sign-in request containing the user's email and password. Cannot be null.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the sign-in operation.</param>
        /// <returns>An <see cref="IActionResult"/> that represents the result of the sign-in operation. Returns 200 OK with a
        /// JWT token and related information if authentication is successful; otherwise, returns 401 Unauthorized.</returns>
        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] SignInRequestDto request, CancellationToken cancellationToken)
        {
            var command = new SignInCommand(
                request.Email,
                request.Password);
            
            var result = await _signInHandler.HandleAsync(command, cancellationToken);

            if (!result.IsSuccessful) return Unauthorized();

            return Ok(new SignInResponseDto(result.IsSuccessful, result.JwtToken, result.ExpiresAt, result.RefreshToken));
        }

        /// <summary>
        /// Handles a request to refresh a JWT access token using a valid refresh token.
        /// </summary>
        /// <param name="request">The refresh token request containing the refresh token to be validated and exchanged for a new access token.
        /// Cannot be null. The refresh token must be provided.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the refresh operation.</param>
        /// <returns>An <see cref="IActionResult"/> containing a new access token, its expiration time, and a new refresh token
        /// if the operation succeeds; otherwise, a bad request result if the refresh token is missing or invalid.</returns>
        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequestDto request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.RefreshToken)) return BadRequest("Refresh token is required");

            var command = new RefreshCommand(
                request.RefreshToken);

            var result = await _refreshHandler.HandleAsync(command, cancellationToken);

            return Ok(new RefreshResponseDto(result.JwtToken, result.ExpiresAt, result.RefreshToken));
        }
    }
}

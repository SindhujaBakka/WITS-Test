using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Services.Contracts;

namespace WITS_Test.Controllers
{
    [ApiController]
    [Route("api/accountusers")]
    public class AccountUsersController : ControllerBase
    {
        private readonly IAccountUsersService _service;

        public AccountUsersController(IAccountUsersService service) => _service = service;

        [HttpGet()]
        public async Task<IActionResult> GetAccountUsers(CancellationToken ct)
        {
            return Ok(await _service.GetAccountUsers(ct));
        }

        [HttpGet("validate")]
        public async Task<IActionResult> Validate([FromQuery] string? username, CancellationToken ct)
        {
            var (isValidFormat, isAvailable, errors) = await _service.ValidateAccountUser(username, ct);
            return Ok(new
            {
                isValidFormat,
                isAvailable,
                errors
            });
        }

        [HttpPost]
        public async Task<IActionResult> Upsert([FromBody] UpsertUsernameRequestDto request, CancellationToken ct)
        {
            var (success, created, error, result) = await _service.UpsertAccountUser(request.Username, request.AccountId, ct);

            if (!success)
            {
                if (error == "Username already taken.")
                    return Conflict(new { message = error });

                return BadRequest(new { message = error });
            }

            if (created)
                return CreatedAtAction(nameof(Validate), new { username = result!.Username }, result);

            return Ok(result);
        }
    }
}

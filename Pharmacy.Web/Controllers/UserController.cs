using Common.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pharmacy.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IIdentityService _identityService;

        public UserController(IUserService userService, IIdentityService identityService)
        {
            _userService = userService;
            _identityService = identityService;
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetMeAsync(CancellationToken cancellationToken)
        {
            var userId = _identityService.GetUserIdentity();
            var user = await _userService.GetByIdAsync(userId, cancellationToken);

            return Ok(user);
        }

        [HttpPost("signUp")]
        public async Task<IActionResult> SignUpAsync(string username, string email, string password)
        {
            var result = await _userService.SignUpAsync(username, email, password);
            if (result.Succeeded)
            {
                return Ok(result.ToString());
            }

            return BadRequest(result.ToString());
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync(string username, string email, CancellationToken cancellationToken)
        {
            var userId = _identityService.GetUserIdentity();
            var result = await _userService.UpdateAsync(userId, username, email, cancellationToken);
            if (result.Succeeded)
            {
                return Ok(result.ToString());
            }

            return BadRequest(result.ToString());
        }

        [Authorize]
        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePasswordAsync(string newPassword, string oldPassword, CancellationToken cancellationToken)
        {
            var userId = _identityService.GetUserIdentity();
            var result = await _userService.ChangePasswordAsync(userId, newPassword, oldPassword, cancellationToken);
            if (result.Succeeded)
            {
                return Ok(result.ToString());
            }

            return BadRequest(result.ToString());
        }

        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAsync(CancellationToken cancellationToken)
        {
            var userId = _identityService.GetUserIdentity();
            var result = await _userService.DeleteAsync(userId, cancellationToken);
            if (result.Succeeded)
            {
                return Ok(result.ToString());
            }

            return BadRequest(result.ToString());
        }
    }
}

using System.Net;
using Domain.ApiResponse;
using Domain.DTOs.AccountDTO;
using Infrastucture.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(IAccountService accountService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<Response<IdentityResult>> RegisterAsync(RegisterDTO register)
    {
        var result = await accountService.RegisterAsync(register);
        if (!result.IsSuccess)
        {
            return new Response<IdentityResult>("Not found", HttpStatusCode.NotFound);
        }
        return new Response<IdentityResult>(null, "Successfuly");
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginDTO login)
    {
        var token = await accountService.LoginAsync(login);
        if (token == null)
        {
            return Unauthorized();
        }
        return Ok(new { token });
    }

    [HttpPost("request-reset-password")]
    public async Task<IActionResult> RequestResetPassword([FromBody] ForgotPasswordDto model)
    {
        var message = await accountService.RequestResetPasswordAsync(model);
        return Ok(message);
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPaswordDTO model)
    {
        var result = await accountService.ResetPasswordAsync(model);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok("Password successfully changed");
    }

    [HttpPost("Change")]
    [Authorize]
    public async Task<Response<string>> ChangePassword(ChangePasswordDTO changePasswordDTO)
    {
        return await accountService.ChangePassword(changePasswordDTO);
    }
}

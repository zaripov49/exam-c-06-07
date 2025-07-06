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

    [HttpPost("Reset")]
    public async Task<Response<string>> ResetPassword(ResetPaswordDTO resetPaswordDTO)
    {
        return await accountService.ResetPassword(resetPaswordDTO);
    }

    [HttpPost("Change")]
    [Authorize]
    public async Task<Response<string>> ChangePassword(ChangePasswordDTO changePasswordDTO)
    {
        return await accountService.ChangePassword(changePasswordDTO);
    }
}

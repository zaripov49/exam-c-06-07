using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Domain.ApiResponse;
using Domain.DTOs.AccountDTO;
using Infrastucture.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastucture.Services;

public class AccountService(UserManager<IdentityUser> userManager,
        IHttpContextAccessor contextAccessor,
        IConfiguration config) : IAccountService
{
    public async Task<Response<IdentityResult>> RegisterAsync(RegisterDTO register)
    {
        var user = new IdentityUser { UserName = register.UserName, Email = register.Email };
        var result = await userManager.CreateAsync(user, register.Password);
        await userManager.AddToRoleAsync(user, "User");
        return new Response<IdentityResult>(result);
    }

    public async Task<string?> LoginAsync(LoginDTO login)
    {
        var user = await userManager.FindByNameAsync(login.UserName);
        if (user == null)
        {
            return null;
        }

        var result = await userManager.CheckPasswordAsync(user, login.Password);
        return !result ? null : await GenerateJwtToken(user);
    }

    public async Task<string> GenerateJwtToken(IdentityUser user)
    {
        var roles = await userManager.GetRolesAsync(user);
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, user.Id),
            new (ClaimTypes.Name, user.UserName!)
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var secretKey = config["Jwt:Key"]!;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<Response<string>> ResetPassword(ResetPaswordDTO resetPaswordDTO)
    {
        var user = await userManager.FindByNameAsync(resetPaswordDTO.EmailOrUserName);
        if (user == null)
        {
            return new Response<string>("User not found", HttpStatusCode.NotFound);
        }

        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        await userManager.ResetPasswordAsync(user, token, resetPaswordDTO.NewPassword);
        return new Response<string>("Successfuly");
    }

    public async Task<Response<string>> ChangePassword(ChangePasswordDTO changePasswordDTO)
    {
        var userName = contextAccessor.HttpContext!.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        var user = await userManager.FindByNameAsync(userName!);
        if (user == null)
        {
            return new Response<string>("UserName not found", HttpStatusCode.NotFound);
        }

        var changePasswordResult = await userManager.ChangePasswordAsync(user, changePasswordDTO.OldPassword, changePasswordDTO.NewPassword);
        if (!changePasswordResult.Succeeded)
        {
            return new Response<string>("Something went wrong", HttpStatusCode.BadRequest);
        }
        return new Response<string>("Successfuly");
    }
}

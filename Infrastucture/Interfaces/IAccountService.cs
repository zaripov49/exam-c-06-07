using Domain.ApiResponse;
using Domain.DTOs.AccountDTO;
using Microsoft.AspNetCore.Identity;

namespace Infrastucture.Interfaces;

public interface IAccountService
{
    Task<Response<IdentityResult>> RegisterAsync(RegisterDTO register);
    Task<string?> LoginAsync(LoginDTO login);
    Task<Response<string>> ResetPassword(ResetPaswordDTO resetPaswordDTO);
    Task<Response<string>> ChangePassword(ChangePasswordDTO changePasswordDTO);
}

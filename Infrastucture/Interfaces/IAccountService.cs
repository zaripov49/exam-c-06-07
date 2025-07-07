using Domain.ApiResponse;
using Domain.DTOs.AccountDTO;
using Microsoft.AspNetCore.Identity;

namespace Infrastucture.Interfaces;

public interface IAccountService
{
    Task<Response<IdentityResult>> RegisterAsync(RegisterDTO register);
    Task<string?> LoginAsync(LoginDTO login);
    Task<string> RequestResetPasswordAsync(ForgotPasswordDto model);
    Task<IdentityResult> ResetPasswordAsync(ResetPaswordDTO model);
    Task<Response<string>> ChangePassword(ChangePasswordDTO changePasswordDTO);
}

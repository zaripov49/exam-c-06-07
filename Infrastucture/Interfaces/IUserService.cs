using Domain.ApiResponse;
using Domain.DTOs.UserDTO;

namespace Infrastucture.Interfaces;

public interface IUserService
{
    Task<Response<List<GetUserDTO>>> GetAllUsersAsync();
    Task<Response<GetUserDTO>> GetUserByIdAsync(string id);
    Task<Response<string>> UpdateUserAsync(UpdateUserDTO updateUserDTO);
    Task<Response<string>> DeleteUserAsync(string id);
}

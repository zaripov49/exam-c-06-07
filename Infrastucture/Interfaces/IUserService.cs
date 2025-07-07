using Domain.ApiResponse;
using Domain.DTOs.UserDTO;
using Domain.Filters;

namespace Infrastucture.Interfaces;

public interface IUserService
{
    Task<PagedResponse<List<GetUserDTO>>> GetAllUsersAsync(UserFilter userFilter);
    Task<Response<GetUserDTO>> GetUserByIdAsync(string id);
    Task<Response<string>> UpdateUserAsync(UpdateUserDTO updateUserDTO);
    Task<Response<string>> DeleteUserAsync(string id);
}

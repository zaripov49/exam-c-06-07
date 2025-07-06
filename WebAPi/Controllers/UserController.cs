using Domain.ApiResponse;
using Domain.DTOs.UserDTO;
using Infrastucture.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet]
    [Authorize("Admin")]
    public async Task<Response<List<GetUserDTO>>> GetAllUsersAsync()
    {
        return await userService.GetAllUsersAsync(); 
    }

    [HttpGet("{id}")]
    [Authorize("Admin")]
    public async Task<Response<GetUserDTO>> GetUserByIdAsync(string id)
    {
        return await userService.GetUserByIdAsync(id); 
    }

    [HttpPut]
    [Authorize]
    public async Task<Response<string>> UpdateUserAsync(UpdateUserDTO updateUserDTO)
    {
        return await userService.UpdateUserAsync(updateUserDTO); 
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<Response<string>> DeleteUserAsync(string id)
    {
        return await userService.DeleteUserAsync(id); 
    }
}

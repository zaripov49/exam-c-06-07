using System.Net;
using AutoMapper;
using Domain.ApiResponse;
using Domain.DTOs.UserDTO;
using Domain.Filters;
using Infrastucture.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture.Services;

public class UserService(UserManager<IdentityUser> userManager, IMapper mapper) : IUserService
{
    public async Task<Response<string>> DeleteUserAsync(string id)
    {
        var deleteUser = await userManager.FindByIdAsync(id);
        if (deleteUser == null)
        {
            return new Response<string>("User not found", HttpStatusCode.NotFound);
        }

        await userManager.DeleteAsync(deleteUser);
        return new Response<string>("Successfuly");
    }

    public async Task<PagedResponse<List<GetUserDTO>>> GetAllUsersAsync(UserFilter userFilter)
    {
        var validFilter = new ValidFilter(userFilter.PageNumber, userFilter.PageSize);
        var query = userManager.Users.AsQueryable();

        if (!string.IsNullOrEmpty(userFilter.UserName))
        {
            query = query.Where(c => c.UserName!.ToLower().Trim().Contains(userFilter.UserName.ToLower().Trim()));
        }

        var totalRecords = await query.CountAsync();

        var paged = await query
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToListAsync();

        var mapped = mapper.Map<List<GetUserDTO>>(paged);
        return new PagedResponse<List<GetUserDTO>>(mapped, totalRecords, validFilter.PageNumber, validFilter.PageSize);
    }

    public async Task<Response<GetUserDTO>> GetUserByIdAsync(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        if (user == null)
        {
            return new Response<GetUserDTO>("User not found", HttpStatusCode.NotFound);
        }

        var result = new GetUserDTO
        {
            Id = user.Id,
            UserName = user.UserName!,
            Email = user.Email!
        };
        return new Response<GetUserDTO>(result);
    }

    public async Task<Response<string>> UpdateUserAsync(UpdateUserDTO updateUserDTO)
    {
        var user = await userManager.FindByIdAsync(updateUserDTO.Id);
        if (user == null)
        {
            return new Response<string>("User not found", HttpStatusCode.NotFound);
        }

        user.UserName = updateUserDTO.UserName;
        user.Email = updateUserDTO.Email;

        // if (!string.IsNullOrEmpty(updateUserDTO.Password))
        // {
        //     var result = await accountService.ChangePassword();
        // }
        var result = await userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            return new Response<string>("Something went wrong", HttpStatusCode.InternalServerError);
        }
        return new Response<string>("Successfuly");
    }

}

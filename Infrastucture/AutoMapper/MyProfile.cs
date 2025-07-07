using AutoMapper;
using Domain.DTOs.UserDTO;
using Microsoft.AspNetCore.Identity;

namespace Infrastucture.AutoMapper;

public class MyProfile : Profile
{
    public MyProfile()
    {
        CreateMap<IdentityUser, GetUserDTO>();
    }
}

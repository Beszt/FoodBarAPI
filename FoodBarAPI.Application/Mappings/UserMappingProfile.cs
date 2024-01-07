using AutoMapper;
using FoodBarAPI.Application.Dtos;
using FoodBarAPI.Domain.Entities;

namespace FoodBarAPI.Application.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>();

        CreateMap<UserDto, User>()
            .ForMember(dest => dest.RoleId, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore());
    }
}
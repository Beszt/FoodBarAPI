using AutoMapper;
using FoodBarAPI.Application.Dtos;
using FoodBarAPI.Domain.Entities;

namespace FoodBarAPI.Application.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, LoginDto>()
            .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.Login))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ReverseMap();
    }
}
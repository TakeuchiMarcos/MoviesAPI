using AutoMapper;
using UserAPI.Data.Dtos;
using UserAPI.Model;

namespace UserAPI.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDto, User>();
        CreateMap<User, ReadUserDto>();
        CreateMap<CreateUserDto, ReadUserDto>();
    }
}

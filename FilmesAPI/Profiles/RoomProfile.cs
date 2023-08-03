using AutoMapper;
using FilmesAPI.Data.Dtos.RoomDtos;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<CreateRoomDto, Room>();
        CreateMap<UpdateRoomDto, Room>();
        CreateMap<Room, ReadRoomDto>()
            .ForMember(roomDto => roomDto.Cinema,
            opt => opt.MapFrom(room => room.Cinema));
    }
}

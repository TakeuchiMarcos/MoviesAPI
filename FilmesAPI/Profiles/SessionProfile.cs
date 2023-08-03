using AutoMapper;
using FilmesAPI.Data.Dtos.SessionDtos;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles;

public class SessionProfile : Profile
{
    public SessionProfile()
    {
        CreateMap<CreateSessionDto, Session>();
        CreateMap<UpdateSessionDto, Session>();
        CreateMap<Session, ReadSessionDto>()
            .ForMember(readSession => readSession.Movie,
            opt => opt.MapFrom(session => session.Movie))
            .ForMember(readSession => readSession.Room,
            opt => opt.MapFrom(session => session.Room));
    }
}

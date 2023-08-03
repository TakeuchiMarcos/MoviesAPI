using AutoMapper;
using FilmesAPI.Data.Dtos.CinemaDtos;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles;

public class CinemaProfile :Profile
{
    public CinemaProfile()
    { 
        CreateMap<CreateCinemaDto, Cinema>();
        CreateMap<UpdateCinemaDto, Cinema>();
        CreateMap<Cinema, ReadCinemaDto>()
            .ForMember(cinemaDto=>cinemaDto.Adress,
            opt=>opt.MapFrom(cinema=>cinema.Adress));
    }
}

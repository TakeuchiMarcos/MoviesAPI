using AutoMapper;
using FilmesAPI.Data.Dtos.MovieDtos;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles;

public class FilmeProfile:Profile
{
    public FilmeProfile()
    {
        CreateMap<CreateMovieDto, Movie>();
        CreateMap<UpdateMovieDto, Movie>();
        CreateMap<Movie, UpdateMovieDto>();
        CreateMap<Movie, ReadMovieDto>();
    }
}

using AutoMapper;
using FilmesAPI.Data.Dtos.AdressDtos;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles;

public class AdressProfile : Profile
{
    public AdressProfile()
    {
        CreateMap<CreateAdressDto, Adress>();
        CreateMap<UpdateAdressDto, Adress>();
        CreateMap<Adress, ReadAdressDto>();
    }
}

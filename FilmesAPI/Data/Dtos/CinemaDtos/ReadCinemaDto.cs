using FilmesAPI.Data.Dtos.AdressDtos;

namespace FilmesAPI.Data.Dtos.CinemaDtos;

public class ReadCinemaDto
{
    public int id { get; set; }
    public string Name { get; set; }
    public ReadAdressDto Adress{ get; set; }
    public DateTime requestTime = DateTime.Now;
}

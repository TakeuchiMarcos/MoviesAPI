using FilmesAPI.Data.Dtos.MovieDtos;
using FilmesAPI.Data.Dtos.RoomDtos;

namespace FilmesAPI.Data.Dtos.SessionDtos;

public class ReadSessionDto
{
    public int Id { get; set; }
    public ReadMovieDto? Movie { get; set; }
    public ReadRoomDto? Room { get; set; }
    public DateTime SessionDate { get; set; }
}

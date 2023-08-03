using FilmesAPI.Data.Dtos.CinemaDtos;

namespace FilmesAPI.Data.Dtos.RoomDtos;

public class ReadRoomDto
{
    public int Id { get; set; }
    public int MaxSeats { get; set; }
    public int OcupiedSeats { get; set; }
    public ReadCinemaDto Cinema { get; set; }
}

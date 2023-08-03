namespace FilmesAPI.Data.Dtos.RoomDtos;

public class UpdateRoomDto
{
    public int MaxSeats { get; set; }
    public int OcupiedSeats { get; set; }
    public int CinemaId { get; set; }
}

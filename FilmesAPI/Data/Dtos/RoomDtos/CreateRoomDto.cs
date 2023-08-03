using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos.RoomDtos;

public class CreateRoomDto
{
    [Required(ErrorMessage = "Room: The field Id is required.")]
    public int? Id { get; set; }
    public int MaxSeats { get; set; }
    public int OcupiedSeats { get; set; }
    [Required(ErrorMessage = "Room: The field CinemaId is required.")]
    public int? CinemaId { get; set; }
}

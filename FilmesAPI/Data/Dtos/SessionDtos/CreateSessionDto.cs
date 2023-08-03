using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmesAPI.Data.Dtos.SessionDtos;

public class CreateSessionDto
{
    [Required(ErrorMessage = "Session: The MovieId field is required.")]
    [ForeignKey("Movie")]
    public int? MovieId { get; set; }

    [Required(ErrorMessage = "Session: the RoomId field is required.")]
    public int? RoomId { get; set; }
    [Required(ErrorMessage = "Session: the CinemaId field is required.")]
    public int? CinemaId { get; set; }

    [Required(ErrorMessage = "Session: the SessionDate field is required.")]
    public DateTime? SessionDate { get; set; }
}

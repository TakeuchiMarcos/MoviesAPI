using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos.CinemaDtos;

public class CreateCinemaDto
{
    [Required(ErrorMessage = "Cinema: The Name field is required.")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Cinema: The AdressId field is required.")]
    public int? AdressId { get; set; }
}

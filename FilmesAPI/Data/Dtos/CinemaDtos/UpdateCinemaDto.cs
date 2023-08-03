using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos.CinemaDtos;

public class UpdateCinemaDto
{
    [Required(ErrorMessage = "Cinema: The Name field is required.")]
    public string Name { get; set; }
}

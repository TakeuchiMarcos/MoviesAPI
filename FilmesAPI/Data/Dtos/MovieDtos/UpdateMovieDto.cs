using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos.MovieDtos;

public class UpdateMovieDto
{
    [Required(ErrorMessage = "Campo título é obrigatório.")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Campo gênero é obrigatório.")]
    public string Genre { get; set; }
    [Required]
    [Range(50, 600, ErrorMessage = "Duração deve ter entre 50 e 600  minutos")]
    public int Duration { get; set; }
}

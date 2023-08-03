using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos.MovieDtos;

public class ReadMovieDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public int Duration { get; set; }
    public DateTime? RequestTime = DateTime.Now;
}

using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models;

public class Movie
{
    internal static int ids = 0;
    [Key]
    [Required]
    public int Id{ get; internal set; }
    [Required(ErrorMessage = "Movie: The Id field is required.")]
    public string Title{ get; set; }
    [Required(ErrorMessage = "Campo gênero é obrigatório.")]
    public string Genre {get; set; }
    [Required]
    [Range(50,600, ErrorMessage ="Duração deve ter entre 50 e 600  minutos")]
    public int Duration { get; set; }
    public virtual ICollection<Session> Sessions { get; set; }
}

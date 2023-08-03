using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models;

public class Cinema
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required(ErrorMessage = "Cinema: The Name field is required.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Cinema: The AdressId field is required.")]
    public int AdressId { get; set; }

    [Required]
    public virtual Adress Adress { get; set; }
    public virtual ICollection<Room> Rooms { get; set; }
}

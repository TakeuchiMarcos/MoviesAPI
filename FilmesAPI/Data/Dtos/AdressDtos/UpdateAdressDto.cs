using FilmesAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos.AdressDtos;

public class UpdateAdressDto
{
    [Required(ErrorMessage = "Street field required")]
    public string Street { get; set; }
    public int Number { get; set; }
    public string neighborhood { get; set; }
    [Required(ErrorMessage = "City field required")]
    public string City { get; set; }
    public string State { get; set; }
    [Required]
    public string Country { get; set; }

    public Cinema Cinema { get; set; }

}

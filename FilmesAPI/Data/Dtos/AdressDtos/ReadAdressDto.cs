using FilmesAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos.AdressDtos;

public class ReadAdressDto
{
    public int Id { get; set; }
    public string Street { get; set; }
    public int? Number { get; set; }
    public string? neighborhood { get; set; }
    public string City { get; set; }
    public string? State { get; set; }
    [Required]
    public string Country { get; set; }
}

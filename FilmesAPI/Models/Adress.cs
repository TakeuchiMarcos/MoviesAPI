using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FilmesAPI.Models;

public class Adress
{
    /// <summary>
    /// Adress inner key
    /// </summary>
    [Key]
    [Required(ErrorMessage = "Id field required")]
    public int Id { get; set; }

    /// <summary>
    /// Main adress line.
    /// </summary>
    [Required(ErrorMessage = "Street field required")]
    public string Street { get; set; }

    /// <summary>
    /// Adress number. May be null
    /// </summary>
    [AllowNull]
    public int? Number { get; set; }

    [AllowNull]
    /// <summary>
    /// Adress complementary information. E.G. Apartment number.
    /// </summary>
    public string? Complement { get; set; }

    [AllowNull]
    public string? neighborhood { get; set; }

    [Required(ErrorMessage = "City field required")]
    public string City { get; set; }

    [AllowNull]
    public string? State { get; set; }

    [Required(ErrorMessage ="Contry field is required")]
    public string Country { get; set; }

    [AllowNull]
    public virtual Cinema? Cinema { get; set; }
}

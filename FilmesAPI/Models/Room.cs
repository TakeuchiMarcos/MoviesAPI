using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmesAPI.Models;

/// <summary>
/// Room is the physical space where the movie is shown.
/// Room identification key is a composition of Id and CinemaId fields in this order.
/// </summary>
public class Room
{
    /// <summary>
    /// Independent part of the composite key.
    /// </summary>
    [Required(ErrorMessage = "Room: The field Id is required.")]
    public int Id { get; set; }

    /// <summary>
    /// CinemaId represents the cinema where this rooms is localized.
    /// </summary>
    [Required(ErrorMessage = "Room: The field CinemaId is required.")]
    [ForeignKey("Cinema")]
    public int CinemaId { get; set; }

    public int MaxSeats { get; set; }
    public int OcupiedSeats { get; set; }

    [Required]
    public virtual Cinema Cinema { get; set; }

    public virtual ICollection<Session> Sessions { get; set; }

}

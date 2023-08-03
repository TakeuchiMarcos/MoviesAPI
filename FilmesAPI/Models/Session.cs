using Castle.Components.DictionaryAdapter;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmesAPI.Models;

public class Session
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required(ErrorMessage = "Session: The MovieId field is required.")]
    [ForeignKey("Movie")]
    public int MovieId { get; set; }
    virtual public Movie? Movie { get; set; }

    [Required(ErrorMessage = "Session: the RoomId field is required.")]
    public int RoomId { get; set; }
    [Required(ErrorMessage = "Session: the CinemaId field is required.")]
    public int CinemaId { get; set; }
    virtual public Room? Room { get; set; }

    [Required(ErrorMessage = "Session: the SessionDate field is required.")]
    public DateTime SessionDate{ get; set; }

}

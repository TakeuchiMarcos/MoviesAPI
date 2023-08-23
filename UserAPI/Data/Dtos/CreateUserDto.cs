using System.ComponentModel.DataAnnotations;

namespace UserAPI.Data.Dtos;

public class CreateUserDto
{
    [Required]
    public string? UserName { get; set; }
    public DateTime? BirthDate { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    [Required]
    [Compare(nameof(Password))]
    public string? RePassword { get; set; }
}

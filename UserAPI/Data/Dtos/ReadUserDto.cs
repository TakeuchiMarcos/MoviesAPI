namespace UserAPI.Data.Dtos;

public class ReadUserDto
{
    public string UserName { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime RecorevedTime = DateTime.Now;
}

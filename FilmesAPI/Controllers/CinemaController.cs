using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.CinemaDtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CinemaController : Controller
{
    protected MovieContext _context;
    protected IMapper _mapper;
    private IStringLocalizer<ErrorMessages> _localizer;

    public CinemaController(MovieContext context, IMapper mapper
        ,IStringLocalizer<Models.Cinema> cinemaLocalizer, IStringLocalizer<ErrorMessages> errorMessagesLocalizer)
    {
        _context = context;
        _mapper = mapper;
        _localizer = errorMessagesLocalizer;
    }

    [HttpGet]
    public IActionResult GetCinema([FromQuery] string? name = null, [FromQuery] int take = 50, [FromQuery] int skip=0)
    {
        if (string.IsNullOrEmpty(name))
        {
            var listTest = _mapper.Map<List<ReadCinemaDto>>(_context.Cinemas.Skip(skip).Take(take).ToList());
            //var listTest = _context.Cinemas.Skip(skip).Take(take).Select(c => _mapper.Map<ReadCinemaDto>(c));
            return Ok(listTest);
        }

        var list = _mapper.Map<List<ReadCinemaDto>>(_context.Cinemas.Where(c => c.Name.ToLower().Contains(name.ToLower())).Skip(skip).Take(take).ToList());

        if (list.Count() <= 0) return NoContent();

        return Ok(list);
    }

    [HttpGet("{id}")]
    public IActionResult GetCinema(int id)
    {
        var cin = _context.Cinemas.Find(id);
        if (cin == null) return NotFound();
        var cinemaDto = _mapper.Map<ReadCinemaDto>(cin);
        return Ok(cinemaDto);
    }

    [HttpPost]
    public IActionResult PostCinema([FromBody] CreateCinemaDto cinemaDto)
    {
        var cinema = _mapper.Map<Cinema>(cinemaDto);
        if(_context.Adresses.Find( cinema.AdressId) == null)
        {
            ModelState.AddModelError(nameof(cinema.AdressId), _localizer["Cinema: The AdressId value is not valid."]);
            return BadRequest(ModelState);
        }
        _context.Cinemas.Add(cinema);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetCinema), new { id = cinema.Id }, _mapper.Map<ReadCinemaDto>(cinema));
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
    {
        var cin = _context.Cinemas.Find(id);
        if (cin == null) return NotFound();
        _mapper.Map(cinemaDto, cin);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCinema(int id)
    {
        var cin = _context.Cinemas.Find(id);
        if(cin == null) return NotFound();
        _context.Cinemas.Remove(cin);
        _context.SaveChanges();
        return NoContent();
    }
}

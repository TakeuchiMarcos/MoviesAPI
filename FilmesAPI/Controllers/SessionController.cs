using AutoMapper;
using FilmesAPI.Data.Dtos.SessionDtos;
using FilmesAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace FilmesAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class SessionController : Controller
{

    protected MovieContext _context;
    protected IMapper _mapper;
    protected IStringLocalizer _localizer;

    public SessionController(MovieContext context, IMapper mapper, IStringLocalizer<ErrorMessages> localizer)
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    [HttpGet]
    public IActionResult GetSession([FromQuery] int? movieId = null, [FromQuery] int? cinemaId = null, [FromQuery] int? roomId = null, [FromQuery] int take = 50, [FromQuery] int skip = 0)
    {
        if (movieId == null && cinemaId == null && roomId == null) return Ok(_mapper.Map<List<ReadSessionDto>>(_context.Sessions.Skip(skip).Take(take).ToList()));
        if (movieId != null && cinemaId != null && roomId != null)
            return GetSession(movieId, cinemaId, roomId);

        var list = _mapper.Map<List<ReadSessionDto>>(_context.Sessions.Where(c => c.MovieId == movieId).Where(c=>c.CinemaId == cinemaId).Where(c=>c.RoomId == roomId)
            .Skip(skip).Take(take).ToList());

        if (list.Count() <= 0) return NoContent();

        return Ok(list);
    }

    [HttpGet("{id}")]
    public IActionResult GetSession(int id)
    {
        var session = _context.Sessions.FirstOrDefault(session => session.Id == id);
        if (session == null) return NotFound();
        var SessionDto = _mapper.Map<ReadSessionDto>(session);
        return Ok(SessionDto);
    }

    [HttpPost]
    public IActionResult PostSession([FromBody] CreateSessionDto SessionDto)
    {
        var session = _mapper.Map<Session>(SessionDto);

        bool hasError = false;
        if (_context.Rooms.Find(session.RoomId, session.CinemaId) == null)
        {
            ModelState.AddModelError("Room", _localizer["Session: RoomId and CinemaId combination is not valid."]);
            hasError = true;
        }
        if (_context.Movies.Find(session.MovieId) == null)
        {
            ModelState.AddModelError("Movie", _localizer["Sesion: MovieId is not valid."]);
            hasError = true;
        }
        if (hasError) return BadRequest(ModelState);

        try
        {
            _context.Sessions.Add(session);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetSession), new { id = session.Id }, _mapper.Map<ReadSessionDto>(session));
        }
        catch (DbUpdateException ex)
        {
            _context.ChangeTracker.Clear();
            ModelState.AddModelError("Database", ex.Message.ToString()+"\n"+ex.InnerException.Message.ToString());
            return BadRequest(ModelState);
        }
        
    }

    [HttpPut("{id}")]
    public IActionResult UpdateSession(int id, [FromBody] UpdateSessionDto SessionDto)
    {
        var session = _context.Sessions.FirstOrDefault(session => session.Id == id);
        if (session == null) return NotFound();
        _mapper.Map(SessionDto, session);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{movieId}/{cinemaId}/{roomId}")]
    public IActionResult DeleteSession(int movieId, int cinemaId, int roomId)
    {
        var cin = _context.Sessions.Find(movieId, cinemaId, roomId);
        if (cin == null) return NotFound();
        _context.Sessions.Remove(cin);
        _context.SaveChanges();
        return NoContent();
    }
}

using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.MovieDtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    protected MovieContext _context;
    protected IMapper _mapper;
    protected IStringLocalizer<ErrorMessages> _localizer;

    public MovieController(MovieContext context, IMapper mapper, IStringLocalizer<ErrorMessages> errorMessagesLocalizer)
    {
        _context = context;
        _mapper = mapper;
        _localizer = errorMessagesLocalizer;
    }

    [HttpPost]
    public IActionResult AdicionaFilme([FromBody] CreateMovieDto filmeDto)
    {
        var filme = _mapper.Map<Movie>(filmeDto);
        _context.Movies.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetFilme), new { id = filme.Id }, _mapper.Map<ReadMovieDto>(filme));
    }

    /// <summary>
    /// Fetch for a list of movies using titulo as a filter. If no filter is used than the full list is returned limited by take and skip parameters.
    /// </summary>
    /// <param name="titulo">Movie title filter.</param>
    /// <param name="take">Max number of movies to be recovered.</param>
    /// <param name="skip">How many movies the search will skip.</param>
    /// <response code="204">If no movie is recovered.</response>
    /// <response code="200">Returns the movie in the body if, at least, one movie is recovered.</response>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetFilme([FromQuery] string? titulo = null,
        [FromQuery] string? cinemaName = null, 
        [FromQuery] int? cinemaId = null,
        [FromQuery] int take = 50, [FromQuery] int skip=0)
    {
        if (string.IsNullOrEmpty(titulo)&& string.IsNullOrEmpty(cinemaName)) 
            return Ok(_mapper.Map<List<ReadMovieDto>>(_context.Movies.Skip(skip).Take(take).ToList()));

        var query = _context.Sessions.AsQueryable();

        if (!string.IsNullOrEmpty(titulo))
            query = query.Where(session => session.Movie.Title.ToLower().Contains(titulo.ToLower()));

        if(cinemaId!=null)
            query = query.Where(session => session.Room.CinemaId==cinemaId);
        else if (!string.IsNullOrEmpty(cinemaName))
            query = query.Where(session => session.Room.Cinema.Name.ToLower().Contains(cinemaName.ToLower()));

        var list = _mapper.Map<List<ReadMovieDto>>(query.Skip(skip).Take(take).Select(session=>session.Movie).ToHashSet());
        Console.WriteLine(list.ToString());
        if (list.Count() <= 0)
            return NoContent();
        return Ok(list);
    }

    [HttpGet("{id}")]
    public IActionResult GetFilme(int id)
    {
        var f = _context.Movies.Find(id);
        if (f == null) return NotFound();
        return Ok(_mapper.Map<ReadMovieDto>(f));
    }
    [HttpPut("{id}")]
    
    public IActionResult UpdateFilme(int id, [FromBody] UpdateMovieDto filmeDto)
    {
        var filme = _context.Movies.Find(id);
        if (filme == null) return NotFound();
        _mapper.Map(filmeDto, filme);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult PatchFilme(int id, [FromBody] JsonPatchDocument<UpdateMovieDto> patch)
    {
        var movie = _context.Movies.Find(id);
        if (movie == null) return NotFound();

        var movieToPatch = _mapper.Map<UpdateMovieDto>(movie);
        patch.ApplyTo(movieToPatch, ModelState);
        if (!TryValidateModel(movieToPatch)) return ValidationProblem(ModelState);

        _mapper.Map(movieToPatch,movie);
        _context.SaveChanges();
        return NoContent();
    }
    [HttpDelete("{id}")]
    public IActionResult DeleteFilme(int id)
    {
        var filme = _context.Movies.Find(id);
        if (filme == null) return NotFound();
        _context.Movies.Remove((Movie)filme);
        _context.SaveChanges();
        return NoContent();
    }
}

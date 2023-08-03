using AutoMapper;
using Castle.Core.Internal;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.RoomDtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System.Globalization;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomController : ControllerBase
{
    protected MovieContext _context;
    protected IMapper _mapper;
    private IStringLocalizer<ErrorMessages> _errorMessagesLocalizer;

    public RoomController(MovieContext context,IMapper mapper, IStringLocalizer<ErrorMessages> errorMessagesLocalizer)
    {
        _context = context;
        _mapper = mapper;
        _errorMessagesLocalizer = errorMessagesLocalizer;
    }

    [HttpGet]
    public IActionResult GetRooms(int take = 50,int skip = 0)
    {
        var list = _context.Rooms.Skip(skip).Take(take).OrderBy(room => room.CinemaId).ToList();
        if (list.Count() <= 0) return NoContent();
        return Ok(_mapper.Map<List<ReadRoomDto>>(list));
    }
    [HttpGet("{id}/{cinemaId}")]
    public IActionResult GetRoom(int id,int cinemaId)
    {
        var room = _context.Rooms.Find(id, cinemaId);
        if(room == null) return NotFound();
        return Ok(_mapper.Map<ReadRoomDto>(room));
    }

    [HttpPost]
    public IActionResult CreateRoom([FromBody] CreateRoomDto createRoomDto)
    {
        var room = _mapper.Map<Models.Room>(createRoomDto);
        var cinema = _context.Cinemas.Find(room.CinemaId);
        bool hasError = false;
        if (cinema == null)
        {
            ModelState.AddModelError(nameof(room.CinemaId), _errorMessagesLocalizer["Room: The CinemaId value is not valid."]);
            hasError = true;
        }
        else room.Cinema = cinema;
        if(room.Id<=0)
        {
            ModelState.AddModelError(nameof(room.Id), _errorMessagesLocalizer["Room: The Id value is not valid."]);
            hasError = true;
        }
        else if(_context.Rooms.Find(room.Id,room.CinemaId) != null) 
        {
            ModelState.AddModelError(nameof(room.Id) + " or " + nameof(room.CinemaId), _errorMessagesLocalizer["Room: Id and CinemaId combination already exists."]);
            hasError = true;
        }

        if(hasError) return BadRequest(ModelState);

        _context.Rooms.Add(room);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetRoom), new { id = room.Id,cinemaId = room.CinemaId }, _mapper.Map<ReadRoomDto>(room));
    }

    [HttpPut("{id}/{cinemaId}")]
    public IActionResult UpdateRoom(int id,int cinemaId,[FromBody] UpdateRoomDto updateRoomDto)
    {
        var room = _context.Rooms.Find(id,cinemaId);
        if (room == null) return NotFound();
        _mapper.Map(updateRoomDto, room);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteRoom(int id)
    {
        var room = _context.Rooms.Find(id);
        if (room == null) return NotFound();
        _context.Rooms.Remove(room);
        _context.SaveChanges();
        return NoContent();
    }

}

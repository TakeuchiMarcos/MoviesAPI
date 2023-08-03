using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using FilmesAPI.Data.Dtos.AdressDtos;
using Microsoft.Extensions.Localization;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AdressController : ControllerBase
{
    protected MovieContext _context;
    protected IMapper _mapper;
    protected IStringLocalizer<ErrorMessages> _localizer;

    public AdressController(MovieContext context, IMapper mapper, IStringLocalizer<ErrorMessages> localizer)
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    [HttpGet]
    public IActionResult GetAdress([FromQuery] string? street = null, [FromQuery] int take = 50, [FromQuery] int skip = 0)
    {
        if (string.IsNullOrEmpty(street)) return Ok(_mapper.Map<List<ReadAdressDto>>(_context.Adresses.Skip(skip).Take(take).ToList()));

        var list = _mapper.Map<List<ReadAdressDto>>( _context.Adresses.Where(c => c.Street.ToLower().Contains(street.ToLower())).Skip(skip).Take(take).ToList());

        if (list.Count() <= 0) return NoContent();

        return Ok(list);
    }

    [HttpGet("{id}")]
    public IActionResult GetAdress(int id)
    {
        var cin = _context.Adresses.Find(id);
        if (cin == null) return NotFound();
        var AdressDto = _mapper.Map<ReadAdressDto>(cin);
        return Ok(AdressDto);
    }

    [HttpPost]
    public IActionResult PostAdress([FromBody] CreateAdressDto AdressDto)
    {
        var Adress = _mapper.Map<Adress>(AdressDto);
        _context.Adresses.Add(Adress);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetAdress), new { id = Adress.Id }, _mapper.Map<ReadAdressDto>(Adress));
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAdress(int id, [FromBody] UpdateAdressDto AdressDto)
    {
        var cin = _context.Adresses.Find(id);
        if (cin == null) return NotFound();
        _mapper.Map(AdressDto, cin);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAdress(int id)
    {
        var cin = _context.Adresses.Find(id);
        if (cin == null) return NotFound();
        _context.Adresses.Remove(cin);
        _context.SaveChanges();
        return NoContent();
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Services;
using UserAPI.Data;
using UserAPI.Data.Dtos;
using UserAPI.Model;

namespace MoviesAPI;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    private IMapper _mapper;
    private UserService _userService;

    public UserController(UserService registerUserService, IMapper mapper)
    {
        _userService = registerUserService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] int take = 50, [FromQuery] int skip = 0)
    {
        var list = await _userService.GetUsersAsync(take, skip);
        if (list == null || list.Count <= 0) NoContent();
        return Ok(list);
    }

    [HttpGet("{userName}")]
    public async Task<IActionResult> GetUser(string userName, [FromQuery] bool test = false)
    {

        var user = await _userService.GetUser(userName);
        if (user == null) return NotFound();

        if (test) return Ok(user);
        return Ok(user);
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
    {
        var result = await _userService.RegisterAsync(userDto);
        if (result.Succeeded) return CreatedAtAction(nameof(GetUser), new { userName = userDto.UserName }, _mapper.Map<ReadUserDto>(userDto));
        foreach (var error in result.Errors) ModelState.AddModelError(error.Code, error.Description);
        return BadRequest(ModelState);

    }
    [HttpPost("signin")]
    public async Task <IActionResult> Login([FromBody] LoginUserDto loginDto)
    {
        var token = await _userService.SignIn(loginDto);
        return Ok(token);
    }

    [HttpDelete("{userName}")]
    public async Task<IActionResult> DeleteUser(string userName)
    {
        var result = await _userService.DeleteUserAsync(userName);
        if (result == null) return NotFound();
        if (result.Succeeded) return NoContent();
        foreach (var error in result.Errors) ModelState.AddModelError(error.Code, error.Description);
        return BadRequest(ModelState);
    }

}

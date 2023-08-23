using AutoMapper;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using UserAPI.Data.Dtos;
using UserAPI.Model;
using UserAPI.Services;

namespace MoviesAPI.Services;

public class UserService
{
    private IMapper _mapper;
    private UserManager<User> _userManager;
    private SignInManager<User> _signInManager;
    private TokenService _tokenService;

    public UserService(IMapper mapper, UserManager<User> userManager, TokenService tokenService, SignInManager<User> signInManager)
    {
        _mapper = mapper;
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
    }

    public async Task<ReadUserDto> GetUser(string userName)
    {
        return _mapper.Map<ReadUserDto>(await GetDbUserAsync(userName));
    }

    public async Task<IdentityResult> RegisterAsync(CreateUserDto userDto)
    {

        var user = _mapper.Map<User>(userDto);
        var result = await _userManager.CreateAsync(user, userDto.Password);
        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userName"></param>
    /// <returns>Returns IdentityResult. If userName is not found return null</returns>
    public async Task<IdentityResult> DeleteUserAsync(string userName)
    {
        var user = await GetDbUserAsync(userName);
        if (user == null) return null;
        return await _userManager.DeleteAsync(user);
    }
    public async Task<List<ReadUserDto>> GetUsersAsync(int take, int skip)
    {
        return _mapper.Map<List<ReadUserDto>>(await _userManager.Users.Take(take).Skip(skip).ToListAsync());
    }

    public async Task<User> GetDbUserAsync(string userName)
    {
        return await _userManager.FindByNameAsync(userName);
    }

    internal async Task<string> SignIn(LoginUserDto loginDto)
    {
        var result = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, false, false);
        if (!result.Succeeded) throw new ArgumentException("UserName or Password invalid.");

        var user = _userManager.Users.FirstOrDefault(u => u.NormalizedUserName.Equals(loginDto.UserName.ToUpper()));


        var token = _tokenService.GenerateToken(user);

        return token;
    }
}

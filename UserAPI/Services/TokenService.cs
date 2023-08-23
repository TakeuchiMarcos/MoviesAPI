using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserAPI.Model;

namespace UserAPI.Services;

public class TokenService
{

    IConfiguration configuration;

    public TokenService(IConfiguration appBuilder)
    {
        configuration = appBuilder;
    }

    public string GenerateToken(User user)
    {
        IEnumerable<Claim> claims = new Claim[]
        {
            new Claim(nameof(user.UserName), user.UserName),
            new Claim(nameof(user.Id),user.Id),
            new Claim(ClaimTypes.DateOfBirth, user.BirthDate.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(configuration["SymmetricSecurityKey"]
            ));

        var signingCredencials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(10),
                claims: claims,
                signingCredentials: signingCredencials
            );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using NadinSoft.Application.Contract.Contracts;

namespace NadinSoft.Infrastructure.Persistance.SQl.Services;

public class TokenService : ITokenService
{
    public string Generate(long userId)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("139199f8-ae6f-447e-9f1a-3cabc187e8ee"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier,userId.ToString()),
        };
        var token = new JwtSecurityToken("https://localhost:7055/", "https://localhost:5232/",
            claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using NadinSoft.Application.Contract.Contracts;
using Microsoft.AspNetCore.Http;


namespace NadinSoft.Infrastructure.Persistance.SQl.Services;

public class UserInfoService : IUserInfoService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserInfoService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public long GetUserIdByToken() 
    {
        var tokenStr = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
        if (tokenStr == null) return new();
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("139199f8-ae6f-447e-9f1a-3cabc187e8ee");
        tokenHandler.ValidateToken(tokenStr, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = false,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
        }, out var validatedToken);
        var jwtToken = (JwtSecurityToken)validatedToken;
        return jwtToken.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => long.Parse(c.Value)).FirstOrDefault();
    }
}
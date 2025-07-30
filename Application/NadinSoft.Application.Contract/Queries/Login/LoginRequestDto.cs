using NadinSoft.Application.Contract.Framework;

namespace NadinSoft.Application.Contract.Queries.Login;

public class LoginRequestDto : IQuery
{
    public string Username { get; set; }
    public string Password { get; set; }
}
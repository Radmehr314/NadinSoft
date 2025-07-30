using NadinSoft.Application.Contract.Framework;

namespace NadinSoft.Application.Contract.Commands.User;

public class AddUserCommand : ICommand
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}
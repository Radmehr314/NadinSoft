using NadinSoft.Application.Contract.Framework;

namespace NadinSoft.Application.Contract.Commands.User;

public class DeleteUserCommand : ICommand
{
    public long Id { get; set; }
}
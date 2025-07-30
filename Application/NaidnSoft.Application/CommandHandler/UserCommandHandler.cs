using NadinSoft.Application.Contract.Commands.User;
using NadinSoft.Application.Contract.Framework;

namespace NaidnSoft.Application.CommandHandler;

public class UserCommandHandler:ICommandHandler<AddUserCommand>,ICommandHandler<UpdateUserCommand>
{
    public Task<CommandResult> Handle(AddUserCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<CommandResult> Handle(UpdateUserCommand command)
    {
        throw new NotImplementedException();
    }
}
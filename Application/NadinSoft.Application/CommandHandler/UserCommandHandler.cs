using NadinSoft.Application.Contract.Commands.User;
using NadinSoft.Application.Contract.Framework;
using NadinSoft.Domain;
using NadinSoft.Domain.Models.User;
using NadinSoft.Application.Mapper;

namespace NadinSoft.Application.CommandHandler;

public class UserCommandHandler:ICommandHandler<AddUserCommand>,ICommandHandler<UpdateUserCommand>
{

    private readonly IUnitOfWork _unitOfWork;

    public UserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<CommandResult> Handle(AddUserCommand command)
    {
        var data = command.Factory();
        await _unitOfWork.UserRepository.Add(data);
        await _unitOfWork.Save();
        return new CommandResult();
    }

    public async Task<CommandResult> Handle(UpdateUserCommand command)
    {
        var user = await _unitOfWork.UserRepository.GetById(command.Id);
        user.Username = command.Username;
        user.Password = command.Password;
        await _unitOfWork.Save();
        return new CommandResult()
        {
            Id = command.Id
        };

    }
}
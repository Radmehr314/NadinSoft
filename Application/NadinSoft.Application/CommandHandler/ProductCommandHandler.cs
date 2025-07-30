using NadinSoft.Application.Contract.Commands.Product;
using NadinSoft.Application.Contract.Contracts;
using NadinSoft.Application.Contract.Framework;
using NadinSoft.Application.Mapper;
using NadinSoft.Domain;

namespace NadinSoft.Application.CommandHandler;

public class ProductCommandHandler : ICommandHandler<AddProductCommand>,ICommandHandler<UpdateProductCommand>,ICommandHandler<DeleteProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;

    public ProductCommandHandler(IUnitOfWork unitOfWork, IUserInfoService userInfoService)
    {
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
    }
    public async Task<CommandResult> Handle(AddProductCommand command)
    {
        var user = await _unitOfWork.UserRepository.GetById(_userInfoService.GetUserIdByToken());
        var product = command.Factory(user.Id,user.Phone,user.Email);
        await _unitOfWork.ProductRepository.Add(product);
        await _unitOfWork.Save();
        return new CommandResult();

    }

    public async Task<CommandResult> Handle(UpdateProductCommand command)
    {
        var product = await _unitOfWork.ProductRepository.GetById(command.Id);
        //ToDo Custom Exception
        if (product.UserId != _userInfoService.GetUserIdByToken())
            throw new Exception("کاربر  ثبت کننده تنها دسترسی ویرایش دارد");
        product.Name = command.Name;
        product.IsAvailable = command.IsAvailable;
        product.ProducedDate = command.ProducedDate;
        await _unitOfWork.Save();
        return new CommandResult() { Id = command.Id };
    }

    public async Task<CommandResult> Handle(DeleteProductCommand command)
    {
        var product = await _unitOfWork.ProductRepository.GetById(command.Id);
        //ToDo Custom Exception
        if (product.UserId != _userInfoService.GetUserIdByToken())
            throw new Exception("کاربر  ثبت کننده تنها دسترسی ویرایش دارد");
        await _unitOfWork.ProductRepository.Delete(command.Id);
        await _unitOfWork.Save();
        return new CommandResult();
    }
}
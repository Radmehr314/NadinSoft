using NadinSoft.Application.Contract.Commands.Product;
using NadinSoft.Application.Contract.Contracts;
using NadinSoft.Application.Contract.Exceptions;
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
        if (product.UserId != _userInfoService.GetUserIdByToken())
            throw new UserAccessException("دسترسی ویرایش محدود است. فقط ثبت ‌کننده اصلی می‌تواند این محصول را ویرایش کند.");
        product.Name = command.Name;
        product.IsAvailable = command.IsAvailable;
        product.ProducedDate = command.ProducedDate;
        await _unitOfWork.Save();
        return new CommandResult() { Id = command.Id };
    }

    public async Task<CommandResult> Handle(DeleteProductCommand command)
    {
        var product = await _unitOfWork.ProductRepository.GetById(command.Id);
        if (product.UserId != _userInfoService.GetUserIdByToken())
            throw new UserAccessException("دسترسی حذف محدود است. فقط ثبت ‌کننده اصلی می‌تواند این محصول را حذف کند.");
        await _unitOfWork.ProductRepository.Delete(command.Id);
        await _unitOfWork.Save();
        return new CommandResult();
    }
}
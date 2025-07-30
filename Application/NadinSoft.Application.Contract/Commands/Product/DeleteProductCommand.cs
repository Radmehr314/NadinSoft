using NadinSoft.Application.Contract.Framework;

namespace NadinSoft.Application.Contract.Commands.Product;

public class DeleteProductCommand : ICommand
{
    public long Id { get; set; }
}
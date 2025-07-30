using NadinSoft.Application.Contract.Framework;

namespace NadinSoft.Application.Contract.Commands.Product;

public class UpdateProductCommand : ICommand
{
    public long Id { get; set; }
    public string Name { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime ProducedDate { get; set; }
}
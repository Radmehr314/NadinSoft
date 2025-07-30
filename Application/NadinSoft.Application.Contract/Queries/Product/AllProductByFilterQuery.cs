using NadinSoft.Application.Contract.Framework;

namespace NadinSoft.Application.Contract.Queries.Product;

public class AllProductByFilterQuery : IQuery
{
    public string? ManufactureEmail { get; set; }
    public string?  ManufacturePhone { get; set; }
}
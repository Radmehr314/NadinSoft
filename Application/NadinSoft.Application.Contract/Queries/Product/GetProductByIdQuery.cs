using NadinSoft.Application.Contract.Framework;

namespace NadinSoft.Application.Contract.Queries.Product;

public class GetProductByIdQuery : IQuery
{
    public long Id { get; set; }
}
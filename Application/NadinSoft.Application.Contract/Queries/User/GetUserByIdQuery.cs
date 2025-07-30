using NadinSoft.Application.Contract.Framework;

namespace NadinSoft.Application.Contract.Queries.User;

public class GetUserByIdQuery : IQuery
{
    public long Id { get; set; }
}
using NadinSoft.Application.Contract.Framework;
using NadinSoft.Application.Contract.Queries.User;
using NadinSoft.Application.Contract.QueryResults.User;

namespace NaidnSoft.Application.QueryHandler;

public class UserQueryHandler : IQueryHandler<GetUserByIdQuery,GetUserByIdQueryResult>,IQueryHandler<GetAllUserQuery,GetAllUserQueryResult>
{
    public Task<GetUserByIdQueryResult> Handle(GetUserByIdQuery query)
    {
        throw new NotImplementedException();
    }

    public Task<GetAllUserQueryResult> Handle(GetAllUserQuery query)
    {
        throw new NotImplementedException();
    }
}
using NadinSoft.Application.Contract.Framework;
using NadinSoft.Application.Contract.Queries.User;
using NadinSoft.Application.Contract.QueryResults.User;
using NadinSoft.Domain;
using NadinSoft.Application.Mapper;

namespace NadinSoft.Application.QueryHandler;

public class UserQueryHandler : IQueryHandler<GetUserByIdQuery,GetUserByIdQueryResult>,IQueryHandler<GetAllUserQuery,List<GetAllUserQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UserQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<GetUserByIdQueryResult> Handle(GetUserByIdQuery query)
    {
        var user = await _unitOfWork.UserRepository.GetById(query.Id);
        return user.GetByIdMapper();
    }

    public async Task<List<GetAllUserQueryResult>> Handle(GetAllUserQuery query)
    {
        var users = await _unitOfWork.UserRepository.All();
        return users.GetAllMapper();
    }
}
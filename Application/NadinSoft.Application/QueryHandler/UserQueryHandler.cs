using NadinSoft.Application.Contract.Contracts;
using NadinSoft.Application.Contract.Exceptions;
using NadinSoft.Application.Contract.Framework;
using NadinSoft.Application.Contract.Queries.Login;
using NadinSoft.Application.Contract.Queries.User;
using NadinSoft.Application.Contract.QueryResults.Login;
using NadinSoft.Application.Contract.QueryResults.User;
using NadinSoft.Domain;
using NadinSoft.Application.Mapper;

namespace NadinSoft.Application.QueryHandler;

public class UserQueryHandler : IQueryHandler<GetUserByIdQuery,GetUserByIdQueryResult>,IQueryHandler<GetAllUserQuery,List<GetAllUserQueryResult>>
,IQueryHandler<LoginRequestDto,LoginDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;

    public UserQueryHandler(IUnitOfWork unitOfWork, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
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

    public async Task<LoginDto> Handle(LoginRequestDto query)
    {
        var user = await _unitOfWork.UserRepository.CheckUserByUsernameAndPassword(query.Username,query.Password);
        if (user == null) throw new NotFoundException("کاربر یافت نشد");

        var permissions = new List<long>();
        var token = _tokenService.Generate(user.Id);

        return new LoginDto()
        {
            Token = token
        };
    }
}
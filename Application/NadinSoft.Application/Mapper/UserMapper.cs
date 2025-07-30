using NadinSoft.Application.Contract.Commands.User;
using NadinSoft.Application.Contract.Queries.User;
using NadinSoft.Application.Contract.QueryResults.User;
using NadinSoft.Domain.Models.Users;

namespace NadinSoft.Application.Mapper;

public static class UserMapper
{
    public static User Factory(this AddUserCommand command)
    {
        return new User()
        {
            Username = command.Username,
            Password = command.Password,
            Email = command.Email,
            Phone = command.Phone
        };
    }

    public static GetUserByIdQueryResult GetByIdMapper(this User user)
    {
        return new GetUserByIdQueryResult()
        {
            Id = user.Id,
            Username = user.Username,
            Password = user.Password,
            Email = user.Email,
            Phone = user.Phone
        };
    }
    
    public static List<GetAllUserQueryResult> GetAllMapper(this List<User> users)
    {
        return users
            .Select(f => new GetAllUserQueryResult() { Id = f.Id, Username = f.Username, Password = f.Password,Email = f.Email,Phone = f.Phone})
            .ToList();
    }
    
}
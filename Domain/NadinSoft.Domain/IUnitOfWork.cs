using NadinSoft.Domain.Models.User;

namespace NadinSoft.Domain;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; set; }
    Task<int> Save();

}
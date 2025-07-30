namespace NadinSoft.Domain.Models.User;

public interface IUserRepository
{
    Task Add(User user);
    Task<User> GetById(long id);
    Task<List<User>> All();
    Task Delete(long id);
}
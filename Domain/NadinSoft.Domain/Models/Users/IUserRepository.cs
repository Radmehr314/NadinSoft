namespace NadinSoft.Domain.Models.Users;

public interface IUserRepository
{
    Task Add(User user);
    Task<User> GetById(long id);
    Task<List<User>> All();
    Task Delete(long id);
    Task<User> CheckUserByUsernameAndPassword(string username, string password);
}
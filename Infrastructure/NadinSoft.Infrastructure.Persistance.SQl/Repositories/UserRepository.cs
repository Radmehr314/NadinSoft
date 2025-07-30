using Microsoft.EntityFrameworkCore;
using NadinSoft.Domain.Models.Users;

namespace NadinSoft.Infrastructure.Persistance.SQl.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public UserRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }

    public async Task Add(User user)=>await _dataBaseContext.AddAsync(user);

    public async Task<User> GetById(long id) => await _dataBaseContext.Users.FindAsync(id);

    public async Task<List<User>> All() => await _dataBaseContext.Users.ToListAsync();
    public async Task Delete(long id) =>  _dataBaseContext.Remove(await GetById((id)));
    public async Task<User> CheckUserByUsernameAndPassword(string username, string password) =>
        await _dataBaseContext.Users.FirstOrDefaultAsync(
            f => f.Username == username && f.Password == password);
}
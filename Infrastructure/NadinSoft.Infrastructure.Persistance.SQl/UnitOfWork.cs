using NadinSoft.Domain;
using NadinSoft.Domain.Models.User;

namespace NadinSoft.Infrastructure.Persistance.SQl;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataBaseContext _dataBaseContext;
    
    public IUserRepository UserRepository { get; set; }

    public UnitOfWork(DataBaseContext dataBaseContext, IUserRepository userRepository)
    {
        _dataBaseContext = dataBaseContext;
        UserRepository = userRepository;
    }
    
    public void Dispose()
    {
        _dataBaseContext.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<int> Save() => await _dataBaseContext.SaveChangesAsync();
}
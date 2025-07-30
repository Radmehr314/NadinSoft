using NadinSoft.Domain;
using NadinSoft.Domain.Models.Products;
using NadinSoft.Domain.Models.Users;

namespace NadinSoft.Infrastructure.Persistance.SQl;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataBaseContext _dataBaseContext;
    
    public IUserRepository UserRepository { get; set; }
    public IProductRepository ProductRepository { get; set; }

    public UnitOfWork(DataBaseContext dataBaseContext, IUserRepository userRepository, IProductRepository productRepository)
    {
        _dataBaseContext = dataBaseContext;
        UserRepository = userRepository;
        ProductRepository = productRepository;
    }
    
    public void Dispose()
    {
        _dataBaseContext.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<int> Save() => await _dataBaseContext.SaveChangesAsync();
}
using NadinSoft.Domain.Models.Products;
using NadinSoft.Domain.Models.Users;

namespace NadinSoft.Domain;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; set; }
    IProductRepository ProductRepository { get; set; }
    Task<int> Save();

}
using Microsoft.EntityFrameworkCore;
using NadinSoft.Domain.Models.Products;

namespace NadinSoft.Infrastructure.Persistance.SQl.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public ProductRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }

    public async Task Add(Product product) => await _dataBaseContext.AddAsync(product);

    public async Task<Product> GetById(long id) => await _dataBaseContext.Products.Include(f=>f.User).FirstOrDefaultAsync(f=>f.Id == id);

    public async Task<List<Product>> All() => await _dataBaseContext.Products.Include(f=>f.User).ToListAsync();

    public async Task Delete(long id) => _dataBaseContext.Products.Remove(await GetById(id));

    public async Task<List<Product>> AllByManufactureEmail(string ManufactureEmail) =>
        await _dataBaseContext.Products.Include(f=>f.User).Where(f => f.ManufactureEmail.Contains(ManufactureEmail)).ToListAsync();

    public async Task<List<Product>> AllByManufacturePhone(string ManufacturePhone) =>
        await _dataBaseContext.Products.Include(f=>f.User).Where(f => f.ManufacturePhone.Contains(ManufacturePhone)).ToListAsync();
}
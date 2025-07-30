namespace NadinSoft.Domain.Models.Products;

public interface IProductRepository
{
    Task Add(Product product);
    Task<Product> GetById(long id);
    Task<List<Product>> All();
    Task Delete(long id);
    Task<List<Product>> AllByManufactureEmail(string ManufactureEmail);
    Task<List<Product>> AllByManufacturePhone(string ManufacturePhone);
}
using NadinSoft.Application.Contract.Commands.Product;
using NadinSoft.Application.Contract.Queries.Product;
using NadinSoft.Application.Contract.QueryResults.Product;
using NadinSoft.Domain.Models.Products;

namespace NadinSoft.Application.Mapper;

public static class ProductMapper
{
    public static Product Factory(this AddProductCommand command,long userId,string ManufacturePhone,string ManufactureEmail)
    {
        return new Product()
        {
            Name = command.Name,
            ProducedDate = DateTime.Now,
            ManufacturePhone = ManufacturePhone,
            ManufactureEmail = ManufactureEmail,
            UserId = userId,
            IsAvailable = command.IsAvailable
        };
    }

    public static GetProductByIdQueryResult GetById(this Product product)
    {
        return new GetProductByIdQueryResult()
        {
            Id = product.Id,
            Name = product.Name,
            ProducedDate = product.ProducedDate,
            IsAvailable = product.IsAvailable,
            ManufactureEmail = product.ManufactureEmail,
            ManufacturePhone = product.ManufacturePhone,
            User = product.User.GetByIdMapper()
        };
    }


    public static List<AllProductsQueryResult> All(this List<Product> products)
    {
        return products.Select(f => new AllProductsQueryResult()
        {
            Id = f.Id, Name = f.Name, ProducedDate = f.ProducedDate, IsAvailable = f.IsAvailable,
            ManufactureEmail = f.ManufactureEmail, ManufacturePhone = f.ManufacturePhone, User = f.User.GetByIdMapper()
        }).ToList();
    }


    public static List<AllProductsByFilterQueryResult> AllByFilter(this List<Product> products)
    {
        return products.Select(f => new AllProductsByFilterQueryResult()
        {
            Id = f.Id, Name = f.Name, ProducedDate = f.ProducedDate, IsAvailable = f.IsAvailable,
            ManufactureEmail = f.ManufactureEmail, ManufacturePhone = f.ManufacturePhone, User = f.User.GetByIdMapper()
        }).ToList();
    }
}
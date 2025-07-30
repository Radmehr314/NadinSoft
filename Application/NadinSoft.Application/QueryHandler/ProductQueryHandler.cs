using NadinSoft.Application.Contract.Framework;
using NadinSoft.Application.Contract.Queries.Product;
using NadinSoft.Application.Contract.QueryResults.Product;
using NadinSoft.Application.Mapper;
using NadinSoft.Domain;
using NadinSoft.Domain.Models.Products;

namespace NadinSoft.Application.QueryHandler;

public class ProductQueryHandler : IQueryHandler<GetProductByIdQuery,GetProductByIdQueryResult>,IQueryHandler<AllProductsQuery,List<AllProductsQueryResult>>,IQueryHandler<AllProductByFilterQuery,List<AllProductsByFilterQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<GetProductByIdQueryResult> Handle(GetProductByIdQuery query)
    {
        var product = await _unitOfWork.ProductRepository.GetById(query.Id);
        return product.GetById();
    }

    public async Task<List<AllProductsQueryResult>> Handle(AllProductsQuery query)
    {
        var products = await _unitOfWork.ProductRepository.All();
        return products.All();
    }

    public async Task<List<AllProductsByFilterQueryResult>> Handle(AllProductByFilterQuery query)
    {
        if (!string.IsNullOrEmpty(query.ManufactureEmail) && !string.IsNullOrEmpty(query.ManufacturePhone))
        {
            throw new Exception("یک فیلتر فقط انتخاب کنید");
        }
        var data = new List<Product>();
        if (!string.IsNullOrEmpty(query.ManufactureEmail))
        {
            data = await _unitOfWork.ProductRepository.AllByManufactureEmail(query.ManufactureEmail);
        }
        else if(!string.IsNullOrEmpty(query.ManufacturePhone))
        {
            data = await _unitOfWork.ProductRepository.AllByManufacturePhone(query.ManufacturePhone);
        }

        return data.AllByFilter();
    }
}
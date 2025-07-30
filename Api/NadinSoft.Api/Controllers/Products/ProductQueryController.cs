using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NadinSoft.Api.Framework;
using NadinSoft.Application.Contract.Framework;
using NadinSoft.Application.Contract.Queries.Login;
using NadinSoft.Application.Contract.Queries.Product;
using NadinSoft.Application.Contract.Queries.User;
using NadinSoft.Application.Contract.QueryResults.Login;
using NadinSoft.Application.Contract.QueryResults.Product;
using NadinSoft.Application.Contract.QueryResults.User;

namespace NadinSoft.Api.Controllers.Products;

public class ProductQueryController : BaseQueryController
{
    public ProductQueryController(IQueryBus bus) : base(bus)
    {
    }
    
    [HttpGet("GetById")]
    public async Task<ActionResult<GetProductByIdQueryResult>> GetUser([FromQuery]GetProductByIdQuery query)
    {
        return Ok(await Bus.Dispatch<GetProductByIdQuery,GetProductByIdQueryResult>(query));
    }
    
    [HttpGet("All")]
    public async Task<ActionResult<List<AllProductsQueryResult>>> AllUser([FromQuery]AllProductsQuery query)
    {
        return Ok(await Bus.Dispatch<AllProductsQuery,List<AllProductsQueryResult>>(query));
    }
    
    [HttpGet("AllByFilter")]
    public async Task<ActionResult<List<AllProductsByFilterQueryResult>>> Login([FromQuery] AllProductByFilterQuery query)
    {
        return Ok(await Bus.Dispatch<AllProductByFilterQuery,List<AllProductsByFilterQueryResult>>(query));

    }
}
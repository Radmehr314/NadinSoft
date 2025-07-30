using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NadinSoft.Api.Framework;
using NadinSoft.Application.Contract.Commands.Product;
using NadinSoft.Application.Contract.Commands.User;
using NadinSoft.Application.Contract.Framework;

namespace NadinSoft.Api.Controllers.Products;

[Authorize]
public class ProductCommandController : BaseCommandController
{
    public ProductCommandController(ICommandBus bus) : base(bus)
    {
    }
    [HttpPost("AddProduct")]
    public async Task<ActionResult<CommandResult>> AddUser([FromBody]AddProductCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }
    
      
    [HttpPut("UpdateProduct")]
    public async Task<ActionResult<CommandResult>> UpdateUser([FromBody]UpdateUserCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }

    [HttpDelete("DeleteProduct")]
    public async Task<ActionResult<CommandResult>> DeleteUser([FromQuery] DeleteProductCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }
    
}
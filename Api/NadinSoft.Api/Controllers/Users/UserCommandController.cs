using Microsoft.AspNetCore.Mvc;
using NadinSoft.Api.Framework;
using NadinSoft.Application.Contract.Commands.User;
using NadinSoft.Application.Contract.Framework;

namespace NadinSoft.Api.Controllers.Users;

public class UserCommandController : BaseCommandController
{
    public UserCommandController(ICommandBus bus) : base(bus)
    {
    }
    
    [HttpPost("AddUser")]
    public async Task<ActionResult<CommandResult>> AddUser([FromBody]AddUserCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }
    
      
    [HttpPut("UpdateUser")]
    public async Task<ActionResult<CommandResult>> UpdateUser([FromBody]UpdateUserCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }

    [HttpDelete("DeleteProduct")]
    public async Task<ActionResult<CommandResult>> DeleteUser([FromQuery] DeleteUserCommand command)
    {
        return Ok(await Bus.Dispatch(command));
    }
}
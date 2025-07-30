using Microsoft.AspNetCore.Mvc;
using NadinSoft.Api.Framework;
using NadinSoft.Application.Contract.Framework;
using NadinSoft.Application.Contract.Queries.User;
using NadinSoft.Application.Contract.QueryResults.User;

namespace NadinSoft.Api.Controllers.Users;

public class UserQueryController : BaseQueryController
{
    public UserQueryController(IQueryBus bus) : base(bus)
    {
    }

    [HttpGet("GetById")]
    public async Task<ActionResult<CommandResult>> GetUser([FromQuery]GetUserByIdQuery query)
    {
        return Ok(await Bus.Dispatch<GetUserByIdQuery,GetUserByIdQueryResult>(query));
    }
    
    [HttpGet("All")]
    public async Task<ActionResult<CommandResult>> AllUser([FromQuery]GetAllUserQuery query)
    {
        return Ok(await Bus.Dispatch<GetAllUserQuery,List<GetAllUserQueryResult>>(query));
    }
}
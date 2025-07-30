using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NadinSoft.Api.Framework;
using NadinSoft.Application.Contract.Framework;
using NadinSoft.Application.Contract.Queries.Login;
using NadinSoft.Application.Contract.Queries.User;
using NadinSoft.Application.Contract.QueryResults.Login;
using NadinSoft.Application.Contract.QueryResults.User;

namespace NadinSoft.Api.Controllers.Users;


[Authorize]
public class UserQueryController : BaseQueryController
{
    public UserQueryController(IQueryBus bus) : base(bus)
    {
    }

    [HttpGet("GetById")]
    public async Task<ActionResult<GetUserByIdQueryResult>> GetUser([FromQuery]GetUserByIdQuery query)
    {
        return Ok(await Bus.Dispatch<GetUserByIdQuery,GetUserByIdQueryResult>(query));
    }
    
    [HttpGet("All")]
    public async Task<ActionResult<List<GetAllUserQueryResult>>> AllUser([FromQuery]GetAllUserQuery query)
    {
        return Ok(await Bus.Dispatch<GetAllUserQuery,List<GetAllUserQueryResult>>(query));
    }
    
    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginDto>> Login([FromBody] LoginRequestDto query)
    {
        return Ok(await Bus.Dispatch<LoginRequestDto,LoginDto>(query));

    }

}
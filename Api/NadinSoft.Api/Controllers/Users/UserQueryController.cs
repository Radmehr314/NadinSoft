using Microsoft.AspNetCore.Mvc;
using NadinSoft.Api.Framework;
using NadinSoft.Application.Contract.Framework;

namespace NadinSoft.Api.Controllers.Users;

public class UserQueryController : BaseQueryController
{
    public UserQueryController(IQueryBus bus) : base(bus)
    {
    }

   
}
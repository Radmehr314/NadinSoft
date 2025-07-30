using NadinSoft.Api.Framework;
using NadinSoft.Application.Contract.Framework;

namespace NadinSoft.Api.Controllers.Users;

public class UserCommandController : BaseCommandController
{
    public UserCommandController(ICommandBus bus) : base(bus)
    {
    }
}
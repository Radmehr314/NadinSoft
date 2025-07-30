
using NadinSoft.Application.Contract.Framework;

namespace NadinSoft.Api.Framework;

public class BaseCommandController:BaseController
{
    protected readonly ICommandBus Bus;
    public BaseCommandController(ICommandBus bus)
    {
        Bus = bus;
    }
}
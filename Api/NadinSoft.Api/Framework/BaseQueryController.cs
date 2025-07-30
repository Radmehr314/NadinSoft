
using NadinSoft.Application.Contract.Framework;

namespace NadinSoft.Api.Framework;

public class BaseQueryController:BaseController
{
    protected readonly IQueryBus Bus;

    public BaseQueryController(IQueryBus bus)
    {
        Bus = bus;
    }
}
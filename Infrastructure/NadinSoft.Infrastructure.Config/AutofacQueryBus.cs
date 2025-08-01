﻿using Autofac;
using NadinSoft.Application.Contract.Framework;

namespace CNadinSoft.Infrastructure.Config;

public class AutofacQueryBus : IQueryBus
{
    private readonly ILifetimeScope _scope;
    public AutofacQueryBus(ILifetimeScope scope)
    {
        _scope = scope;
    }

    public async Task<TQueryResult> Dispatch<TQuery,TQueryResult>(TQuery query) where TQuery : IQuery 
    {
        var handler = _scope.Resolve<IQueryHandler<TQuery, TQueryResult>>();
        return await handler.Handle(query);
    }
}
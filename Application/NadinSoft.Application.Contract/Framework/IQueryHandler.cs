﻿namespace NadinSoft.Application.Contract.Framework;


public interface IQueryHandler<in TQuery,TQueryResult> where TQuery : IQuery 
{
    Task<TQueryResult> Handle(TQuery query);
}
namespace NadinSoft.Application.Contract.Framework;

public interface IQueryBus
{
    Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query) where TQuery : IQuery ;
}
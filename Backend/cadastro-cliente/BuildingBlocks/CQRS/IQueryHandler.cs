using MediatR;
using static BuildingBlocks.CQRS.IQuery;

namespace BuildingBlocks.CQRS
{
    public interface IQueryHandler
    {
        public interface IQueryHandler<in TQuery, TResponse>
        : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
        where TResponse : notnull
        {
        }
    }
}

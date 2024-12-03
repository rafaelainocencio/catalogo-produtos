using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface IQuery
    {
        public interface IQuery<out TResponse> : IRequest<TResponse>
        where TResponse : notnull
        {
        }
    }
}

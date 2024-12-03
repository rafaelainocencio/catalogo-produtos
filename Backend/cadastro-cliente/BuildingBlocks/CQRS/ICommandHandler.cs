using MediatR;
using static BuildingBlocks.CQRS.ICommand;

namespace BuildingBlocks.CQRS
{
    public interface ICommandHandler
    {
        public interface ICommandHandler<in TCommand>
       : ICommandHandler<TCommand, Unit>
       where TCommand : ICommand<Unit>
        {
        }

        public interface ICommandHandler<in TCommand, TResponse>
            : IRequestHandler<TCommand, TResponse>
            where TCommand : ICommand<TResponse>
            where TResponse : notnull
        {
        }
    }
}

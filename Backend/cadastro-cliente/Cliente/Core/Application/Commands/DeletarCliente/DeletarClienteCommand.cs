using Application.Responses;
using static BuildingBlocks.CQRS.ICommand;

namespace Application.Commands.DeletarCliente
{
    public class DeletarClienteCommand : ICommand<ClienteResponse>
    {
        public DeletarClienteCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}

using Application.Responses;
using static BuildingBlocks.CQRS.ICommand;

namespace Application.Commands.AtivarCliente
{
    public class AtivarClienteCommand : ICommand<ClienteResponse>
    {
        public AtivarClienteCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}

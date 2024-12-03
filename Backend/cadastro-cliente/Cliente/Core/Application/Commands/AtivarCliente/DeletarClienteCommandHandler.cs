using Application.Responses;
using Domain.Cliente.Ports;
using static BuildingBlocks.CQRS.ICommandHandler;

namespace Application.Commands.AtivarCliente
{
    public class AtivarClienteCommandHandler : ICommandHandler<AtivarClienteCommand, ClienteResponse>
    {
        private readonly IClienteRepository _clienteRepository;

        public AtivarClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ClienteResponse> Handle(AtivarClienteCommand request, CancellationToken cancellationToken)
        {
            var clienteExistente = await _clienteRepository.ObterPorId(request.Id);

            if (clienteExistente == null)
            {
                return new ClienteResponse
                {
                    ErrorCode = ErrorCodes.CLIENTE_NAO_ENCONTRADO,
                    Mensage = "Cliente não encontrado.",
                    Success = false
                };
            }

            clienteExistente.Ativar();

            await _clienteRepository.Atualizar(clienteExistente);

            return new ClienteResponse
            {
                Success = true,
                Mensage = "Cliente ativado com sucesso."
            };
        }
    }
}

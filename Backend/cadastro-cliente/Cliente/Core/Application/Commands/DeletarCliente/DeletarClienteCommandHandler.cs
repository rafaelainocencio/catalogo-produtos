using Application.Responses;
using Domain.Cliente.Ports;
using static BuildingBlocks.CQRS.ICommandHandler;

namespace Application.Commands.DeletarCliente
{
    public class DeletarClienteCommandHandler : ICommandHandler<DeletarClienteCommand, ClienteResponse>
    {
        private readonly IClienteRepository _clienteRepository;

        public DeletarClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ClienteResponse> Handle(DeletarClienteCommand request, CancellationToken cancellationToken)
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

            clienteExistente.Desativar();

            await _clienteRepository.Atualizar(clienteExistente);

            return new ClienteResponse
            {
                Success = true,
                Mensage = "Cliente desativado com sucesso."
            };
        }
    }
}

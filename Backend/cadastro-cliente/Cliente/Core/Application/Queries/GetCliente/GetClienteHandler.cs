using Application.Queries.GetCliente;
using Application.Responses;
using Domain.Cliente.Ports;
using static Application.Responses.ClienteResponse;
using static BuildingBlocks.CQRS.IQueryHandler;

namespace Application.Queries.GetFornecedor
{
    public class GetClienteHandler : IQueryHandler<GetClienteQuery, ClienteResponse>
    {
        private readonly IClienteRepository _clienteRepository;

        public GetClienteHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ClienteResponse> Handle(GetClienteQuery query, CancellationToken cancellationToken)
        {
            var cliente = await _clienteRepository.ObterPorId(query.Id);

            if (cliente == null)
            {
                return new ClienteResponse
                {
                    ErrorCode = ErrorCodes.CLIENTE_NAO_ENCONTRADO,
                    Mensage = "Cliente não encontrado",
                    Success = false
                };
            }

            return new ClienteResponse
            {
                Data = new ResponseData
                {
                    Id = cliente.Id,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Email = cliente.Email,
                    DocumentoNumero = cliente.Documento.Numero,
                    DocumentoTipo = (int)cliente.Documento.Tipo,
                },
                Success = true
            };
        }
    }
}

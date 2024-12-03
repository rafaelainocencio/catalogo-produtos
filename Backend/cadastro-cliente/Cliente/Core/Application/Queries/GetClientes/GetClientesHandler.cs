using Application.Queries.GetCliente;
using Application.Responses;
using Domain.Cliente.Ports;
using System.Collections.Generic;
using static Application.Responses.ClienteResponse;
using static BuildingBlocks.CQRS.IQueryHandler;

namespace Application.Queries.GetClientes
{
    public class GetClientesHandler : IQueryHandler<GetClientesQuery, ClienteResponse>
    {
        private readonly IClienteRepository _clienteRepository;

        public GetClientesHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ClienteResponse> Handle(GetClientesQuery query, CancellationToken cancellationToken)
        {
            var clientes = await _clienteRepository.ObterTodos(query.Desativado);

            if (clientes == null)
            {
                return new ClienteResponse
                {
                    ErrorCode = ErrorCodes.CLIENTE_NAO_ENCONTRADO,
                    Mensage = "Nenhum cliente encontrado",
                    Success = false
                };
            }

            List<ResponseData> responseData = new List<ResponseData>();

            foreach (var cliente in clientes)
            {
                responseData.Add(new ResponseData
                {
                    Id = cliente.Id,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Email = cliente.Email,
                    DocumentoNumero = cliente.Documento.Numero,
                    DocumentoTipo = (int)cliente.Documento.Tipo,
                });
            }

            return new ClienteResponse
            {
                MultipleData = responseData,
                Success = true,
            };
        }
    }
}

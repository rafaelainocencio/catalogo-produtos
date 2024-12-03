using Application.Responses;
using Domain.Cliente.Enums;
using Domain.Cliente.Exception;
using Domain.Cliente.Ports;
using Domain.Cliente.ValueObjects;
using static Application.Responses.ClienteResponse;
using static BuildingBlocks.CQRS.ICommandHandler;

namespace Application.Commands.AtualizarCliente
{
    public class AtualizarClienteCommandHandler : ICommandHandler<AtualizarClienteCommand, ClienteResponse>
    {
        private readonly IClienteRepository _clienteRepository;

        public AtualizarClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ClienteResponse> Handle(AtualizarClienteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var cliente = await _clienteRepository.ObterPorId(request.Id);

                if (cliente == null || cliente.Desativado)
                {
                    return new ClienteResponse
                    {
                        ErrorCode = ErrorCodes.CLIENTE_NAO_ENCONTRADO,
                        Mensage = "Cliente não existe ou está desativado",
                        Success = false
                    };
                }

                var clienteExistente = await _clienteRepository.ObterPorDocumento(request.DocumentoNumero);

                if (clienteExistente != null && clienteExistente.Id != request.Id)
                {
                    return new ClienteResponse
                    {
                        ErrorCode = ErrorCodes.CLIENTE_EXISTENTE,
                        Mensage = "Já existe cliente com o mesmo documento.",
                        Success = false
                    };
                }


                cliente.Update(request.Nome,
                               request.Sobrenome,
                               request.Email,
                               new Documento(request.DocumentoNumero,
                               (TipoDocumento)request.DocumentoTipo));

                await cliente.Save(_clienteRepository);

                return new ClienteResponse
                {
                    SingleData = new ResponseData
                    {
                        Id = cliente.Id,
                        Nome = cliente.Nome,
                        Sobrenome = cliente.Sobrenome,
                        Email = cliente.Email,
                        DocumentoNumero = cliente.Documento.Numero,
                        DocumentoTipo = (int)cliente.Documento.Tipo,
                    },
                    Success = true,

                };
            }
            catch (ClienteInvalidoException ex)
            {
                return new ClienteResponse
                {
                    ErrorCode = ErrorCodes.CLIENTE_INVALIDO,
                    Mensage = ex.Message,
                    Success = false
                };
            }
            catch (EmailInvalidoException ex)
            {
                return new ClienteResponse
                {
                    ErrorCode = ErrorCodes.EMAIL_INVALIDO,
                    Mensage = ex.Message,
                    Success = false
                };
            }
        }
    }
}

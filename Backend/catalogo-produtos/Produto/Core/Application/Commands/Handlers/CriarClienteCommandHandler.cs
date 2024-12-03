using Application.Commands;
using Application.Responses;
using Domain.Cliente;
using Domain.Cliente.Enums;
using Domain.Cliente.Exception;
using Domain.Cliente.Ports;
using Domain.Cliente.ValueObjects;
using MediatR;
using static Application.Responses.ClienteResponse;

namespace Application.Commands.Handlers
{
    public class CriarClienteCommandHandler : IRequestHandler<CriarClienteCommand, ClienteResponse>
    {
        private readonly IClienteRepository _clienteRepository;

        public CriarClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ClienteResponse> Handle(CriarClienteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var cliente = new Cliente(request.Nome,
                                      request.Sobrenome,
                                      request.Email,
                                      new Documento(request.DocumentoNumero, (TipoDocumento)request.DocumentoTipo));


                await cliente.Save(_clienteRepository);



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
            catch (DocumentoInvalidoException ex)
            {
                return new ClienteResponse
                {
                    ErrorCode = ErrorCodes.DOCUMENTO_INVALIDO,
                    Mensage = ex.Message,
                    Success = false
                };
            }
            catch (Exception ex)
            {
                return new ClienteResponse
                {
                    ErrorCode = ErrorCodes.NAO_FOI_POSSIVEL_ARMAZENAR_DADOS,
                    Mensage = ex.Message,
                    Success = false
                };
            }
        }
    }
}

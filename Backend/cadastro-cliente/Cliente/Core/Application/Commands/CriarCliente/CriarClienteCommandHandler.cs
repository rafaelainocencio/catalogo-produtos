﻿using Application.Responses;
using Domain.Cliente;
using Domain.Cliente.Enums;
using Domain.Cliente.Exception;
using Domain.Cliente.Ports;
using Domain.Cliente.ValueObjects;
using static Application.Responses.ClienteResponse;
using static BuildingBlocks.CQRS.ICommandHandler;

namespace Application.Commands.CriarCliente
{
    public class CriarClienteCommandHandler : ICommandHandler<CriarClienteCommand, ClienteResponse>
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

                var clienteExistente = await _clienteRepository.ObterPorDocumento(request.DocumentoNumero);

                if (clienteExistente != null)
                {
                    return new ClienteResponse
                    {
                        ErrorCode = ErrorCodes.CLIENTE_EXISTENTE,
                        Mensage = "Já existe cliente com o mesmo documento.",
                        Success = false
                    };
                }

                var cliente = new Cliente(request.Nome,
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
                        Desativado = cliente.Desativado,
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

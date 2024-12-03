using Application.Commands;
using Application.Responses;
using Domain.Cliente;
using Domain.Cliente.Enums;
using Domain.Cliente.Ports;
using Domain.Cliente.ValueObjects;
using MediatR;

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

            var cliente = new Cliente(request.Nome,
                                      request.Sobrenome,
                                      request.Email,
                                      new Documento(request.DocumentoNumero, (TipoDocumento)request.DocumentoTipo));


            await cliente.Save(_clienteRepository);



            return new ClienteResponse 
            {

                Id = cliente.Id,
                Nome = cliente.Nome,
                Sobrenome = cliente.Sobrenome,
                Email = cliente.Email,
                DocumentoNumero = cliente.Documento.Numero,
                DocumentoTipo = (int)cliente.Documento.Tipo,
                Success = true,
            };
        }
    }
}

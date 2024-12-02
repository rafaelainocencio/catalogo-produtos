using Application.Commands;
using Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Handlers
{
    public class CriarClienteCommandHandler : IRequestHandler<CriarClienteCommand, ClienteResponse>
    {
        public Task<ClienteResponse> Handle(CriarClienteCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

using Application.Commands;
using Application.Requests;
using Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Ports
{
    public interface IClienteManager
    {
        Task<ClienteResponse> CriarCliente(CriarClienteRequest request);
    }
}

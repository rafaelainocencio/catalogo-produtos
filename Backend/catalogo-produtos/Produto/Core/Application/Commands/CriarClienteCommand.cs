using Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class CriarClienteCommand : IRequest<ClienteResponse>
    {
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public string Email { get; private set; }
        public string DocumentoNumero { get; private set; }
        public string DocumentoTipo { get; private set; }

        public CriarClienteCommand(string nome, string sobrenome, string email, string documentoNumero, string documentoTipo)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Email = email;
            DocumentoNumero = documentoNumero;
            DocumentoTipo = documentoTipo;
        }
    }
}

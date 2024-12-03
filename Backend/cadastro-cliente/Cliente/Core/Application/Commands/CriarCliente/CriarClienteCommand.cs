using Application.Responses;
using static BuildingBlocks.CQRS.ICommand;

namespace Application.Commands.CriarCliente
{
    public class CriarClienteCommand : ICommand<ClienteResponse>
    {
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public string Email { get; private set; }
        public string DocumentoNumero { get; private set; }
        public int DocumentoTipo { get; private set; }

        public CriarClienteCommand(string nome, string sobrenome, string email, string documentoNumero, int documentoTipo)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Email = email;
            DocumentoNumero = documentoNumero;
            DocumentoTipo = documentoTipo;
        }
    }
}

using Application.Responses;
using static BuildingBlocks.CQRS.ICommand;

namespace Application.Commands.AtualizarCliente
{
    public class AtualizarClienteCommand : ICommand<ClienteResponse>
    {
        public Guid Id { get; set; }
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public string Email { get; private set; }
        public string DocumentoNumero { get; private set; }
        public int DocumentoTipo { get; private set; }

        public AtualizarClienteCommand(Guid id, string nome, string sobrenome, string email, string documentoNumero, int documentoTipo)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            Email = email;
            DocumentoNumero = documentoNumero;
            DocumentoTipo = documentoTipo;
        }
    }
}

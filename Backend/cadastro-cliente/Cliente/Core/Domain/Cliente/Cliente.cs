using Domain.Cliente.Exception;
using Domain.Cliente.Ports;
using Domain.Cliente.ValueObjects;

namespace Domain.Cliente
{
    public class Cliente
    {
        public Cliente()
        {
        }

        public Cliente(string nome, string sobrenome, string email, Documento documento)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Email = email;
            Documento = documento;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public Documento Documento { get; set; }

        private void ValidateState()
        {
            if (Documento == null)
            {
                throw new DocumentoInvalidoException("Documento não pode ser nulo");
            }

            Documento.ValidateState();

            if (string.IsNullOrEmpty(Nome))
            {
                throw new ClienteInvalidoException("Nome não pode ser nulo ou vazio.");
            }
            if (string.IsNullOrEmpty(Sobrenome))
            {
                throw new ClienteInvalidoException("Sobrenome não pode ser nulo ou vazio.");
            }
            if (!Utils.ValidateEmail(Email))
            {
                throw new EmailInvalidoException();
            }
        }

        public async Task Save(IClienteRepository clienteRepository)
        {
            ValidateState();

            if (Id == Guid.Empty)
            {
                Id = await clienteRepository.Adicionar(this);
            }
            else
            {
                await clienteRepository.Atualizar(this);
            }
        }
    }
}

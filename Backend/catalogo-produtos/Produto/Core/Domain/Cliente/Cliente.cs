using Domain.Cliente.Exception;
using Domain.Cliente.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Cliente
{
    public class Cliente
    {
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
    }
}

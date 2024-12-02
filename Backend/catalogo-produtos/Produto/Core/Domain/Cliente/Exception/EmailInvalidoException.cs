using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Cliente.Exception
{
    internal class EmailInvalidoException : System.Exception
    {
        public EmailInvalidoException() : base("Email inválido.")
        {
        }
    }
}

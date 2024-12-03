using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Cliente.Exception
{
    public class ClienteInvalidoException : System.Exception
    {
        public ClienteInvalidoException(string? message) : base(message)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Cliente.Exception
{
    public class DocumentoInvalidoException : System.Exception
    {
        public DocumentoInvalidoException(string? message) : base(message)
        {
        }
    }
}

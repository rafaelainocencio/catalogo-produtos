using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.ViewModels
{
    public class ClienteViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DocumentoViewModel Documento{ get; set; }

        public class DocumentoViewModel
        {
            public string Numero { get; set; }
            public int Tipo { get; set; }
        }
    }
}

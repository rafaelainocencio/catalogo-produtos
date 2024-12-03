namespace API.Controllers.Clientes
{
    public class ClienteInputModel
    {
        public class CriarCliente
        {
            public string Nome { get; set; }
            public string Sobrenome { get; set; }
            public string Email { get; set; }
            public TipoDocumentoInputModel Documento { get; set; }

            public class TipoDocumentoInputModel
            {
                public string Numero { get; set; }
                public int Tipo { get; set; }
            }
        }
    }
}

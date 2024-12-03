namespace Application.Responses
{
    public class ClienteResponse : Response
    {
        public ResponseData? SingleData { get; set; }
        public List<ResponseData>? MultipleData { get; set; }

        public class ResponseData
        {
            public Guid Id { get; set; }
            public string Nome { get; set; }
            public string Sobrenome { get; set; }
            public string Email { get; set; }
            public string DocumentoNumero { get; set; }
            public int DocumentoTipo { get; set; }
        }
    }
}

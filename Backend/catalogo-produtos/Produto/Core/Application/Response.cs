namespace Application
{
    public abstract class Response
    {
        public bool Success { get; set; }
        public string Mensage { get; set; }
        public ErrorCodes ErrorCode { get; set; }
    }
    public enum ErrorCodes
    {
        NAO_ENCONTRADO = 1,
        NAO_FOI_POSSIVEL_ARMAZENAR_DADOS = 2,
        DOCUMENTO_INVALIDO = 3,
        INFORMACAO_OBRIGATORIA_AUSENTE = 4,
        EMAIL_INVALIDO = 5,
        CLIENTE_NAO_ENCONTRADO = 6,

        CLIENTE_INVALIDO = 99,

    }
}

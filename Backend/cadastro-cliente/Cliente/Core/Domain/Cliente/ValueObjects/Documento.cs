using Domain.Cliente.Enums;
using Domain.Cliente.Exception;
using System.Text.RegularExpressions;

namespace Domain.Cliente.ValueObjects
{
    public class Documento
    {
        public Documento(string numero, TipoDocumento tipo)
        {
            Numero = numero;
            Tipo = tipo;
        }

        public string Numero { get; set; }
        public TipoDocumento Tipo { get; set; }

        public void ValidateState()
        {
            if (string.IsNullOrEmpty(Numero))
            {
                throw new DocumentoInvalidoException("Número do documento não pode ser nulo ou vazio.");
            }
            if (Tipo == 0)
            {
                throw new DocumentoInvalidoException("Tipo do documento inválido.");
            }
            if (Tipo == TipoDocumento.CPF && Numero.Length != 11)
            {
                throw new DocumentoInvalidoException("CPF deve conter 11 dígitos.");
            }
            if (Tipo == TipoDocumento.RG && Numero.Length != 9)
            {
                throw new DocumentoInvalidoException("RG deve conter 9 dígitos.");
            }
            if (Tipo == TipoDocumento.RG && ContainsLetterOrSpecialCharacter(Numero))
            {
                throw new DocumentoInvalidoException("RG não pode conter letras ou caracteres especiais.");
            }
            if (Tipo == TipoDocumento.CPF && !IsCpf(Numero))
            {
                throw new DocumentoInvalidoException("CPF não é válido.");
            };

        }

        public bool ContainsLetterOrSpecialCharacter(string str)
        {
            return Regex.IsMatch(str, @"[a-zA-Z]") || Regex.IsMatch(str, @"[^a-zA-Z0-9\s]");
        }

        public static bool IsCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
}

﻿using Domain.Cliente.Enums;
using Domain.Cliente.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Cliente.ValueObjects
{
    public class Documento
    {
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

        }
    }
}
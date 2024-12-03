﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Cliente.Ports
{
    public interface IClienteRepository
    {
        Task<Cliente> ObterPorId(Guid id);
        Task<Cliente> ObterPorEmail(string email);
        Task<Cliente> ObterPorDocumento(string documento);
        Task<IEnumerable<Cliente>> ObterTodos();
        Task<Guid> Adicionar(Cliente cliente);
        Task Atualizar(Cliente cliente);
        Task Remover(Guid id);
    }
}
﻿using Domain.Cliente;
using Domain.Cliente.Ports;

namespace Data.Clientes
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Adicionar(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente.Id;
        }

        public Task Atualizar(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public Task<Cliente> ObterPorDocumento(string documento)
        {
            throw new NotImplementedException();
        }

        public Task<Cliente> ObterPorEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Cliente> ObterPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Cliente>> ObterTodos()
        {
            throw new NotImplementedException();
        }

        public Task Remover(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

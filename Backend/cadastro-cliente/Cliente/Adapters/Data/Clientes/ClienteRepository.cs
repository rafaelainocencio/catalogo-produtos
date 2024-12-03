using Domain.Cliente;
using Domain.Cliente.Ports;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Guid> Atualizar(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
            return cliente.Id;
        }

        public async Task<Cliente> ObterPorDocumento(string documento)
        {
            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.Documento.Numero == documento);

            return cliente;
        }

        public Task<Cliente> ObterPorEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<Cliente> ObterPorId(Guid id)
        {
            var cliente =  await _context.Clientes
                                  .FirstOrDefaultAsync(c => c.Id == id);

            return cliente;
        }

        public async Task<IEnumerable<Cliente>> ObterTodos(bool? desativado)
        {
            if(desativado == null)
            {
                var clientes = _context.Clientes;
                return clientes;
            }
            else
            {
                var clientes = _context.Clientes.Where(c => c.Desativado == desativado); //ver isso aqui pq posso querer todos
                return clientes;
            }
        }
         
        public Task Remover(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Cliente.Ports
{
    public interface IClienteRepository
    {
        Task<Cliente> ObterPorId(Guid id);
        Task<Cliente> ObterPorDocumento(string documento);
        Task<IEnumerable<Cliente>> ObterTodos(bool? desativado);
        Task<Guid> Adicionar(Cliente cliente);
        Task<Guid> Atualizar(Cliente cliente);
    }
}

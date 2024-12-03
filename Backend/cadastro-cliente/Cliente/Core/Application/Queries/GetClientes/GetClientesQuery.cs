using Application.Responses;
using static BuildingBlocks.CQRS.IQuery;

namespace Application.Queries.GetClientes
{
    public class GetClientesQuery : IQuery<ClienteResponse>
    {
        public GetClientesQuery(bool desativado) 
        {
            Desativado = desativado;
        }
        public bool Desativado { get; set; }
    }
}

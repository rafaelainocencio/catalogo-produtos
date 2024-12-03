using Application.Responses;
using static BuildingBlocks.CQRS.IQuery;

namespace Application.Queries.GetCliente
{
    public class GetClienteQuery : IQuery<ClienteResponse>
    {
        public GetClienteQuery(Guid id) 
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}

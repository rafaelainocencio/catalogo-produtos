using Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        ILogger<ClienteController> _logger;
        private readonly IMediator _mediator;

        public ClienteController(ILogger<ClienteController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Create(ClienteInputModel.CriarCliente cliente)
        {
            var command = new CriarClienteCommand(cliente.Nome,
                                                  cliente.Sobrenome,
                                                  cliente.Email,
                                                  cliente.Documento.Numero,
                                                  cliente.Documento.Tipo);
            var res = await _mediator.Send(command);

            if (res.Success) return Ok(res);

            return BadRequest(res);
            //depois implementar o tratamento de erro.
        }
    }
}

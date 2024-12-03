using Application;
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

            if (res.ErrorCode == ErrorCodes.NAO_FOI_POSSIVEL_ARMAZENAR_DADOS)
            {
                return BadRequest(res);
            }
            if (res.ErrorCode == ErrorCodes.CLIENTE_INVALIDO)
            {
                return BadRequest(res);
            }
            if (res.ErrorCode == ErrorCodes.DOCUMENTO_INVALIDO)
            {
                return BadRequest(res);
            }
            if (res.ErrorCode == ErrorCodes.EMAIL_INVALIDO)
            {
                return BadRequest(res);
            }

            _logger.LogError("Erro ao criar cliente", res);
            return BadRequest(500);
        }
    }
}

using Application;
using Application.Commands.CriarCliente;
using Application.Queries.GetCliente;
using Application.Queries.GetClientes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Clientes
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        ILogger<ClienteController> _logger;
        private readonly ISender _sender;

        public ClienteController(ILogger<ClienteController> logger,
            ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }
        
        [HttpGet("")]
        public async Task <IActionResult> Get([FromQuery] bool? desativado)
        {
            var res = await _sender.Send(new GetClientesQuery(desativado));

            if (res.Success)
            {
                return Ok(res);
            }

            if (res.ErrorCode == ErrorCodes.CLIENTE_NAO_ENCONTRADO)
            {
                return NotFound(res);
            }

            _logger.LogError("Erro ao criar cliente", res);
            return BadRequest(500);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var res = await _sender.Send(new GetClienteQuery(id));

            if (res.Success) return Ok(res);

            if (res.ErrorCode == ErrorCodes.CLIENTE_NAO_ENCONTRADO)
            {
                return NotFound(res);
            }

            _logger.LogError("Erro ao buscar cliente", res);
            return BadRequest(500);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ClienteInputModel.CriarCliente cliente)
        {
            var command = new CriarClienteCommand(cliente.Nome,
                                                  cliente.Sobrenome,
                                                  cliente.Email,
                                                  cliente.Documento.Numero,
                                                  cliente.Documento.Tipo);

            var res = await _sender.Send(command);

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

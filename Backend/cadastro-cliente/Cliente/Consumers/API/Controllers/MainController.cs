using BuildingBlocks.CQRS;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MainController : ControllerBase
    {
        protected ICommandHandler _commandHandler;

        protected MainController (ICommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Application.Commands;

namespace TicketSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsCommandController : ControllerBase
    {
        private readonly IMediator mediator;

        public TicketsCommandController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketCommand command)
        {
            var ticket = await mediator.Send(command);
            return Ok(ticket);
        }
        [HttpPost("handle/{id}")]
        public async Task<IActionResult> HandleTicket(Guid id)
        {
            var command = new HandleTicketCommand { Id = id };
            var ticket = await mediator.Send(command);
            return Ok(ticket);
        }
    }
}

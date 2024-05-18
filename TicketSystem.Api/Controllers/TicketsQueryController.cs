using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Application.Queries;

namespace TicketSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsQueryController : ControllerBase
    {
        private readonly IMediator mediator;

        public TicketsQueryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetTickets(int pageNumber = 1, int pageSize = 5)
        {
            var query = new GetTicketsQuery { PageNumber = pageNumber, PageSize = pageSize };
            var tickets = await mediator.Send(query);
            return Ok(tickets);
        }
    }
}

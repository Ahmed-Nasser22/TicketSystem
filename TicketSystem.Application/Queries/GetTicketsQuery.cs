using MediatR;
using TicketSystem.Domain.Models;

namespace TicketSystem.Application.Queries
{
    public class GetTicketsQuery : IRequest<IEnumerable<Ticket>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

}

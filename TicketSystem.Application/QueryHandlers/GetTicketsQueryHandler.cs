using MediatR;
using TicketSystem.Application.Interfaces;
using TicketSystem.Application.Queries;
using TicketSystem.Domain.Models;

namespace TicketSystem.Application.QueryHandlers
{
    public class GetTicketsQueryHandler : IRequestHandler<GetTicketsQuery, IEnumerable<Ticket>>
    {
        private readonly ITicketRepository ticketRepository;

        public GetTicketsQueryHandler(ITicketRepository ticketRepository)
        {
            this.ticketRepository = ticketRepository;
        }

        public async Task<IEnumerable<Ticket>> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
        {
            var result = await ticketRepository.GetTicketsAsync(request.PageNumber, request.PageSize);
            return result;
        }
    }
}
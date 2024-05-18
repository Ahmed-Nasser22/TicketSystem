using MediatR;
using TicketSystem.Application.Commands;
using TicketSystem.Application.Interfaces;

namespace TicketSystem.Application.CommandHandlers
{
    public class HandleTicketsBackgroundJobCommandHandler : IRequestHandler<HandleTicketsBackgroundJobCommand>
    {
        private readonly ITicketRepository ticketRepository;

        public HandleTicketsBackgroundJobCommandHandler(ITicketRepository ticketRepository)
        {
            this.ticketRepository = ticketRepository;
        }

        public async Task Handle(HandleTicketsBackgroundJobCommand request, CancellationToken cancellationToken)
        {

            await ticketRepository.HandleDueTicketsAsync();
        }
    }
}

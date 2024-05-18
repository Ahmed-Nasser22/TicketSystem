using MediatR;
using TicketSystem.Application.Commands;
using TicketSystem.Application.Interfaces;
using TicketSystem.Domain.Enums;
using TicketSystem.Domain.Models;

namespace TicketSystem.Application.CommandHandlers
{
    public class HandleTicketCommandHandler : IRequestHandler<HandleTicketCommand, Ticket>
    {
        private readonly ITicketRepository ticketRepository;

        public HandleTicketCommandHandler(ITicketRepository ticketRepository)
        {
            this.ticketRepository = ticketRepository;
        }

        public async Task<Ticket> Handle(HandleTicketCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var ticket = await ticketRepository.GetTicketByIdAsync(request.Id);
                ticket.IsHandled = true;
                ticket.Status = TicketStatus.Handled;
                await ticketRepository.UpdateTicketAsync(ticket);
                return ticket;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }

        }
    }
}

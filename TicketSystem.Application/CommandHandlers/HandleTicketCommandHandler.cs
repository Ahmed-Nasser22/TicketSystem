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
            var result = ticketRepository.GetTicketByIdAsync(request.Id);
           
                var ticket = result.Result;
                ticket.IsHandled = true;
                ticket.Status = TicketStatus.Handled;
               await ticketRepository.UpdateTicketAsync(ticket);
               return ticket;
            
        }
    }
}

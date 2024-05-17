using TicketSystem.Application.Commands;
using TicketSystem.Application.Interfaces;
using TicketSystem.Domain.Models;

namespace TicketSystem.Application.CommandHandlers
{
    public class CreateTicketCommandHandler
    {
        private readonly ITicketRepository ticketRepository;

        public CreateTicketCommandHandler(ITicketRepository ticketRepository)
        {
            this.ticketRepository = ticketRepository;
        }

        public Task<Ticket> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = new Ticket
            {
                Id = new Guid(),
                CreationDateTime = DateTime.Now,
                PhoneNumber = request.PhoneNumber,
                Governorate = request.Governorate,
                City = request.City,
                District = request.District,
                Status = "New"
            };
            ticketRepository.AddTicket(ticket);
            return Task.FromResult(ticket);
        }
    }
}

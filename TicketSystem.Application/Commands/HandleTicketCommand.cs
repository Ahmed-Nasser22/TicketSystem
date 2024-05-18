using MediatR;
using TicketSystem.Domain.Models;

namespace TicketSystem.Application.Commands
{
    public class HandleTicketCommand : IRequest<Ticket>
    {
        public Guid Id { get; set; }

        public HandleTicketCommand(Guid id)
        {
            Id = id;
        }
    }
}

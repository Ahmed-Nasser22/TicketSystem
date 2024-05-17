using MediatR;
using TicketSystem.Domain.Models;

namespace TicketSystem.Application.Commands
{
    public class CreateTicketCommand : IRequest<Ticket>
    {
        public string PhoneNumber { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }
        public string District { get; set; }
    }
}

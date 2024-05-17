using TicketSystem.Domain.Models;

namespace TicketSystem.Application.Interfaces
{
    public interface ITicketRepository
    {
        void AddTicket(Ticket ticket);
        IEnumerable<Ticket> GetTickets(int pageNumber, int pageSize);
        Ticket GetTicketById(int id);
        void UpdateTicket(Ticket ticket);
    }
}

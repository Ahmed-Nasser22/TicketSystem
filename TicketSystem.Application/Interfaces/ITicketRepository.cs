using TicketSystem.Domain.Models;

namespace TicketSystem.Application.Interfaces
{
        public interface ITicketRepository
        {
            Task AddTicketAsync(Ticket ticket);
            Task<IEnumerable<Ticket>> GetTicketsAsync(int pageNumber, int pageSize);
            Task<Ticket> GetTicketByIdAsync(int id);
            Task UpdateTicketAsync(Ticket ticket);
        }
}

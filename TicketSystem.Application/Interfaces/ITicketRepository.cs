using TicketSystem.Domain.Models;

namespace TicketSystem.Application.Interfaces
{
        public interface ITicketRepository
        {
            Task<Ticket> AddTicketAsync(Ticket ticket);
            Task<Ticket> UpdateTicketAsync(Ticket ticket);
            Task<IEnumerable<Ticket>> GetTicketsAsync(int pageNumber, int pageSize);
            Task<Ticket> GetTicketByIdAsync(Guid id);
        }
}

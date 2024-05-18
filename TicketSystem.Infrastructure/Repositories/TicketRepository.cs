using Microsoft.EntityFrameworkCore;
using TicketSystem.Application.Interfaces;
using TicketSystem.Domain;
using TicketSystem.Domain.Models;

namespace TicketSystem.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext context;

        public TicketRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task AddTicketAsync(Ticket ticket)
        {
            await context.Tickets.AddAsync(ticket);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Ticket>> GetTicketsAsync(int pageNumber, int pageSize)
        {
            return await context.Tickets
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Ticket> GetTicketByIdAsync(Guid id)
        {
            return await context.Tickets.FindAsync(id);
        }

        public async Task UpdateTicketAsync(Ticket ticket)
        {
            context.Entry(ticket).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
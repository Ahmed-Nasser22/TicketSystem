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

        public async Task<Ticket> AddTicketAsync(Ticket ticket)
        {
            var result = context.Tickets.Add(ticket);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Ticket> UpdateTicketAsync(Ticket ticket)
        {
            context.Entry(ticket).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return ticket;
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
            var ticket = await context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                throw new KeyNotFoundException("Ticket Not Found");
            }
            return ticket;
        }

    }
}
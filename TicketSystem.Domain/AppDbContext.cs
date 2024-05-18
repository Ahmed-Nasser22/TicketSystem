using Microsoft.EntityFrameworkCore;
using TicketSystem.Domain.Models;

namespace TicketSystem.Domain
{
    public class AppDbContext : DbContext
    {
        public DbSet<Ticket> Tickets { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}

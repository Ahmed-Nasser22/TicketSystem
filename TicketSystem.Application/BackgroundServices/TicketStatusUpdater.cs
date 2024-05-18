using Microsoft.Extensions.Hosting;
using TicketSystem.Application.Interfaces;
using TicketSystem.Domain.Enums;

public class TicketStatusUpdater : IHostedService
{
    private Timer? timer;
    private readonly ITicketRepository ticketRepository;
    private readonly TimeSpan interval = TimeSpan.FromHours(1); // Interval set to 1 hour

    public TicketStatusUpdater(ITicketRepository ticketRepository)
    {
        this.ticketRepository = ticketRepository;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Delay the initial execution by the remaining time within the hour
        var delay = DateTime.UtcNow.AddHours(1).Date - DateTime.UtcNow;
        timer = new Timer(UpdateTicketStatuses, null, delay, interval);
        return Task.CompletedTask;
    }

    private async void UpdateTicketStatuses(object? state)
    {
        var tickets = await ticketRepository.GetTicketsAsync(1, int.MaxValue);
        foreach (var ticket in tickets)
        {
            var elapsed = DateTime.UtcNow - ticket.CreationDateTime;
            if (elapsed.TotalMinutes >= 60)
            {
                ticket.IsHandled = true;
                ticket.Status = TicketStatus.Handled;
                await ticketRepository.UpdateTicketAsync(ticket);
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        timer?.Dispose();
    }
}

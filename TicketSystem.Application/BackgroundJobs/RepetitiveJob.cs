using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TicketSystem.Application.BackgroundJobs
{
    public class RepetitiveJob<TCommand> : IHostedService
        where TCommand : class, IRequest, new()
    {
        private Timer timer;
        private readonly int timerSchedule;
        public IServiceScopeFactory serviceScopeFactory;

        public RepetitiveJob(int repeatInSeconds, IServiceScopeFactory serviceScopeFactory)
        {
            timerSchedule = (int)new TimeSpan(0, 0, repeatInSeconds).TotalMilliseconds;
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(async _ => await OnTimerFiredAsync(cancellationToken),
                null, timerSchedule, timerSchedule);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Dispose();
            return Task.CompletedTask;
        }
        private async Task OnTimerFiredAsync(CancellationToken cancellationToken)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var jobName = typeof(TCommand).Name;
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<RepetitiveJob<TCommand>>>();
                try
                {
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    var myCommand = new TCommand();
                    logger.LogInformation("{RepetitiveJob} started at : {now}", jobName, DateTime.UtcNow.ToString("o"));
                    await mediator.Send(myCommand, cancellationToken);
                    logger.LogInformation("{RepetitiveJob} Ended at : {now}", jobName, DateTime.UtcNow.ToString("o"));

                }
                catch (Exception ex)
                {
                    logger.LogCritical($"{{RepetitiveJob}} Exception at : {DateTime.UtcNow:o} Error: {ex.Message}, Exception: {ex}");
                }
                finally
                {
                    logger.LogInformation("{RepetitiveJob} scheduling new instance  after : {timerSchedule} Mills", jobName, timerSchedule);
                }
            }
        }
    }
}

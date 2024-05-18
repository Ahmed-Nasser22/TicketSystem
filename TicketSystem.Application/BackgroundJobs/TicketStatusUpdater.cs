using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TicketSystem.Application.Interfaces;

namespace TicketSystem.Application.BackgroundJobs
{
    public class RepetitiveJob<TCommand> : IHostedService 
        where TCommand : class , IRequest , new ()
    {
        private Timer timer;
        private readonly int timerSchedule;
        private readonly IMediator mediator;
        private readonly ILogger<RepetitiveJob<TCommand>> logger;
   
        public RepetitiveJob(IMediator mediator , ILogger<RepetitiveJob<TCommand>> logger, int repeatInSeconds )
        {
            this.mediator = mediator;
            this.logger = logger;
            timerSchedule = (int)new TimeSpan(0, 0, repeatInSeconds).TotalMilliseconds;
        }

        public  async Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(async _=> await OnTimerFiredAsync(cancellationToken),
                null , timerSchedule , Timeout.Infinite);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Dispose();
            return Task.CompletedTask;
        }
        private async Task OnTimerFiredAsync(CancellationToken cancellationToken)
        {
                var jobName = typeof(TCommand).Name;
                try
                {
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

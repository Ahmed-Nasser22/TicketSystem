using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TicketSystem.Application.Interfaces;

namespace TicketSystem.Application.BackgroundJobs
{
    public  static class RegisterBackgroundHostingStartup
    {
        public static IServiceCollection RegisterRepetitiveJob<TCommand>(this IServiceCollection services, int repeatInSeconds)
          where TCommand : class, IRequest, new()
        {
            services.AddHostedService(sp =>
            {

                    var logger = sp.GetRequiredService<ILogger<RepetitiveJob<TCommand>>>();
                var mediator = sp.GetRequiredService<IMediator>();
                return new RepetitiveJob<TCommand>(mediator, logger, repeatInSeconds);
            });
            return services;
        }
    }
}

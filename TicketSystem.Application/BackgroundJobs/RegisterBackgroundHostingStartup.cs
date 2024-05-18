using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TicketSystem.Application.BackgroundJobs
{
    public  static class RegisterBackgroundHostingStartup
    {
        public static IServiceCollection RegisterRepetitiveJob<TCommand>(this IServiceCollection services, int repeatInSeconds)
          where TCommand : class, IRequest, new()
        {
            services.AddHostedService(sp =>
            {
                var serviceScopeFactory  = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();
                return new RepetitiveJob<TCommand>(repeatInSeconds , serviceScopeFactory);
            });
            return services;
        }
    }
}

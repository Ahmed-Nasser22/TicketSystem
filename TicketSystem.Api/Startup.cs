using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TicketSystem.Application.BackgroundJobs;
using TicketSystem.Application.Commands;
using TicketSystem.Application.Interfaces;
using TicketSystem.Domain;
using TicketSystem.Infrastructure.Repositories;

namespace TicketSystem.Api
{
    public class Startup
    {

        public void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            }); ;
            RegisterServices(builder);
            // Swagger configuration
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }
        private void RegisterServices(WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddScoped<ITicketRepository, TicketRepository>();

            builder.Services.AddMediatR(cfg =>
            {
                    cfg.Lifetime = ServiceLifetime.Scoped;
                    cfg.RegisterServicesFromAssemblyContaining<HandleTicketsBackgroundJobCommand>();
            });

            // Register background job for hanlding tickets after 1 hour of creation.
            builder.Services.RegisterRepetitiveJob<HandleTicketsBackgroundJobCommand>(3600);
        }
        public void ConfigurePipeLine(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

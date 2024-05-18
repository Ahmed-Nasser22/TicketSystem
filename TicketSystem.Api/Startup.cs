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

        public void ConfigureServices(WebApplicationBuilder  builder)
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
            // Entity Framework Core configuration
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddScoped<ITicketRepository, TicketRepository>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
            }
            // Register background job for hanlding tickets after 1 hour of creation.
            builder.Services.RegisterRepetitiveJob<HandleTicketsBackgroundJobCommand>(10);
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

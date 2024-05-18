using Microsoft.EntityFrameworkCore;
using TicketSystem.Application.Interfaces;
using TicketSystem.Domain;
using TicketSystem.Infrastructure.Repositories;

namespace TicketSystem.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method is called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Entity Framework Core configuration
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));
            RegisterServices(services);
            // Swagger configuration
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ITicketRepository, TicketRepository>();
        }
        // This method is called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        }
}

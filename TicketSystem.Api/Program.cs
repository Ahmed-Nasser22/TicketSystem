using TicketSystem.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var startup = new Startup();
        startup.ConfigureServices(builder);

        var app = builder.Build();
        startup.ConfigurePipeLine(app);
        app.Run();
    }
}

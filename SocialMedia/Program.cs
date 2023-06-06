using SocialMedia.Infrustructure;
using SocialMedia.Application;
using Serilog;

internal class Program
{
    private static void Main(string[] args)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        var builder = WebApplication.CreateBuilder(args);
        Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();
        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddApplicationService();
        builder.Services.AddResponseCaching();
        builder.Services.AddOutputCache();
        builder.Services.AddLazyCache();
        builder.Services.AddMemoryCache();
        builder.Services.AddInfrastructureService(builder.Configuration);
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(x=>
            x.DisplayRequestDuration());
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseResponseCaching();
        app.UseOutputCache();
        app.MapControllers();
       
        app.Run();
    }
}
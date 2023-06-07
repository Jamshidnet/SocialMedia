using SocialMedia.Infrustructure;
using SocialMedia.Application;
using Serilog;
using System.Threading.RateLimiting;

internal class Program
{
    private static void Main(string[] args)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
                    factory: partition => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = 10,
                        QueueLimit = 0,
                        Window = TimeSpan.FromMinutes(1)
                    }));
        });

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
                app.UseSwaggerUI(x =>
                x.DisplayRequestDuration());
            }
            app.UseRouting();
            app.UseRateLimiter();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseResponseCaching();
            app.UseOutputCache();
            app.MapControllers();

            app.Run();
            
        
   }
}
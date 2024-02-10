
using Microsoft.EntityFrameworkCore;
using MyMoneyManager.API.Extensions;
using MyMoneyManager.API.Middlewares;
using MyMoneyManager.Data.DbContexts;
using MyMoneyManager.Service.Mappers;
using MyMoneyManager.Shared.Helpers;
using Serilog;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<AppDbContext>(option
            => option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCustomServices();
        EnvoronmentHelper.WebRootPath = Path.GetFullPath("wwwroot");

        //Logger
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .CreateLogger();
        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(logger);

        builder.Services.AddAutoMapper(typeof(MapperProfile));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseMiddleware<ExceptionHandlerMiddleWare>();
        app.UseAuthorization();
        app.UseStaticFiles();

        app.MapControllers();

        app.Run();
    }
}

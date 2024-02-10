
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using MyMoneyManager.API.Extensions;
using MyMoneyManager.API.Middlewares;
using MyMoneyManager.API.Models;
using MyMoneyManager.Data.DbContexts;
using MyMoneyManager.Service.Mappers;
using MyMoneyManager.Shared.Helpers;
using Newtonsoft.Json;
using Serilog;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        /// Fix the Cycle
        builder.Services.AddControllers()
             .AddNewtonsoftJson(options =>
             {
                 options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
             });

        builder.Services.AddDbContext<AppDbContext>(option
            => option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // CORS
        builder.Services.ConfigureCors();

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

        builder.Services.AddControllers(options =>
        {
            options.Conventions.Add(new RouteTokenTransformerConvention(
                                            new ConfigurationApiUrlName()));
        });

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

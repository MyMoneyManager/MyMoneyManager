using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MyMoneyManager.Data.IRepositories;
using MyMoneyManager.Data.Repositories;
using MyMoneyManager.Service.Interfaces.IAboutUsServices;
using MyMoneyManager.Service.Interfaces.IAuthorizations;
using MyMoneyManager.Service.Interfaces.ICategoryServices;
using MyMoneyManager.Service.Interfaces.IFeedbacks;
using MyMoneyManager.Service.Interfaces.IGoalService;
using MyMoneyManager.Service.Interfaces.IReportService;
using MyMoneyManager.Service.Interfaces.ITransactionServices;
using MyMoneyManager.Service.Interfaces.Users;
using MyMoneyManager.Service.Services.AboutServices;
using MyMoneyManager.Service.Services.Authorizations;
using MyMoneyManager.Service.Services.CategoryServices;
using MyMoneyManager.Service.Services.FeedbackServices;
using MyMoneyManager.Service.Services.GoalServices;
using MyMoneyManager.Service.Services.ReportServices;
using MyMoneyManager.Service.Services.TransactionServices;
using MyMoneyManager.Service.Services.Users;
using System.Text;

namespace MyMoneyManager.API.Extensions;

public static class ServiceExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        // Generic Reporitory
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        // User Service
        services.AddScoped<IUserService, UserService>();

        // AboutUs Service
        services.AddScoped<IAboutUsService, AboutUsService>();

        // AboutUsAssets
        services.AddScoped<IAboutUsAssetService, AboutUsAssetService>();

        // Feedback Service
        services.AddScoped<IFeedbackService,FeedbackService>();

        // Goal Service
        services.AddScoped<IGoalService,GoalService>();

        // Report Service
        services.AddScoped<IReportServie, ReportService>();

        // Tranzaction Service
        services.AddScoped<ITranzactionService, TranzactionService>();

        // Category Service
        services.AddScoped<ICategoryService, CategoryService>();

        // Authorizations
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IRolePermissionService, RolePermissionService>();
        services.AddScoped<IRoleService, RoleService>();
    }

    /// <summary>
    /// Add JWT credentials from appsettings.json and configure it
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddJwtService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                ClockSkew = TimeSpan.Zero
            };
        });
    }

    /// <summary>
    /// Add CORS to give access for header, actions
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
        });
    }
}
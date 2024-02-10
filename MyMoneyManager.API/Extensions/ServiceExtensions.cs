﻿using MyMoneyManager.Data.IRepositories;
using MyMoneyManager.Data.Repositories;
using MyMoneyManager.Service.Interfaces.IAboutUsServices;
using MyMoneyManager.Service.Interfaces.IFeedbacks;
using MyMoneyManager.Service.Interfaces.IGoalService;
using MyMoneyManager.Service.Interfaces.IReportService;
using MyMoneyManager.Service.Interfaces.Users;
using MyMoneyManager.Service.Services.AboutServices;
using MyMoneyManager.Service.Services.FeedbackServices;
using MyMoneyManager.Service.Services.GoalServices;
using MyMoneyManager.Service.Services.ReportServices;
using MyMoneyManager.Service.Services.Users;

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
    }
}
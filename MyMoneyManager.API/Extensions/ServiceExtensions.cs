using MyMoneyManager.Data.IRepositories;
using MyMoneyManager.Data.Repositories;

namespace MyMoneyManager.API.Extensions;

public static class ServiceExtensions
{
    public static void AddService(this IServiceCollection services)
    {
        // Generic Reporitory
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }
}
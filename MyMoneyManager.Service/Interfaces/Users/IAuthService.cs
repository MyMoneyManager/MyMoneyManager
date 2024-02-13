using MyMoneyManager.Service.DTOs.Logins;
using System.Threading.Tasks;

namespace MyMoneyManager.Service.Interfaces.Users;

public interface IAuthService
{
    Task<LoginResultDto> AuthenticateAsync(string email, string password);
}

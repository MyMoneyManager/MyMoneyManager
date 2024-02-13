using Microsoft.AspNetCore.Mvc;
using MyMoneyManager.Service.DTOs.Logins;
using MyMoneyManager.Service.Interfaces.Users;

namespace MyMoneyManager.API.Controllers.AuthorizationsController;

public class AuthorizationsController : BaseController
{
    private readonly IAuthService authService;

    public AuthorizationsController(IAuthService authService)
    {
        this.authService = authService;
    }
    [HttpPost("authenticate")]
    public async Task<IActionResult> AuthenticateAsync(LoginDto dto)
    {
        return Ok(await this.authService.AuthenticateAsync(dto.Email, dto.Password));
    }
}

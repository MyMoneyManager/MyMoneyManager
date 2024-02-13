using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyMoneyManager.Domain.Entities;
using MyMoneyManager.Service.DTOs.Logins;
using MyMoneyManager.Service.Exceptions;
using MyMoneyManager.Service.Interfaces.IAuthorizations;
using MyMoneyManager.Service.Interfaces.Users;
using MyMoneyManager.Shared.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyMoneyManager.Service.Services.Users;

public class AuthService : IAuthService
{
    private readonly IUserService userService;
    private readonly IConfiguration configuration;
    private readonly IRoleService roleService;

    public AuthService(IUserService userService, IConfiguration configuration, IRoleService roleService)
    {
        this.userService = userService;
        this.configuration = configuration;
        this.roleService = roleService;
    }

    public async Task<LoginResultDto> AuthenticateAsync(string email, string password)
    {
        var user = await userService.RetrieveByEmailAsync(email);
        if (user == null || !PasswordHelper.Verify(password, user.Password))
            throw new CustomException(400, "Email or password is incorrect");

        var role = await this.roleService.RetrieveByIdForAuthAsync(user.RolId);
        user.Role = role;
        return new LoginResultDto
        {
            Token = GenerateToken(user)
        };
    }

    private string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                 new Claim("Id", user.Id.ToString()),
                 new Claim(ClaimTypes.Role, user.Role.Name.ToString()),
                 new Claim(ClaimTypes.Name, user.FirstName)
            }),
            Audience = configuration["JWT:Audience"],
            Issuer = configuration["JWT:Issuer"],
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(configuration["JWT:Expire"])),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

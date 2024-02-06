using MyMoneyManager.Domain.Enums;
using System.ComponentModel;

namespace MyMoneyManager.Service.DTOs.Users;

public class UserForResultDto
{
    public long Id { get; set; }

    [DisplayName("First Name")]
    public string FirstName { get; set; }

    [DisplayName("LastName")]
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserGenderType GenderType { get; set; }
}

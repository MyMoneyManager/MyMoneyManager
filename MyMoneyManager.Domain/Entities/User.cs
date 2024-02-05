using MyMoneyManager.Domain.Commons;
using MyMoneyManager.Domain.Enums;

namespace MyMoneyManager.Domain.Entities;

public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserGenderType GenderType { get; set; }
}

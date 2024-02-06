using MyMoneyManager.Domain.Commons;
using MyMoneyManager.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MyMoneyManager.Domain.Entities;

public class User : Auditable
{
    [MinLength(3), MaxLength(50)]
    public string FirstName { get; set; }

    [MinLength(3), MaxLength(50)]
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserGenderType GenderType { get; set; }
}

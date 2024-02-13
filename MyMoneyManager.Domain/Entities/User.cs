using MyMoneyManager.Domain.Commons;
using MyMoneyManager.Domain.Entities.Authorizations;
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
    public long RolId { get; set; }
    public Role Role { get; set; }

    public IEnumerable<Wallet> Wallets { get; set; }
    public IEnumerable<Goal> Goals { get; set; }
    public IEnumerable<Remainder> Remainders { get; set; }
    public IEnumerable<Report> Reports { get; set; }
    public IEnumerable<Feedback> Feedbacks { get; set; }
}

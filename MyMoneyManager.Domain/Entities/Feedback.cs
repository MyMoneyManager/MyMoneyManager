using MyMoneyManager.Domain.Commons;

namespace MyMoneyManager.Domain.Entities;

public class Feedback : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public string Message { get; set; }
}

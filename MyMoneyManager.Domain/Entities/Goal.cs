using MyMoneyManager.Domain.Commons;

namespace MyMoneyManager.Domain.Entities;

public class Goal : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public string Name { get; set; }
    public decimal TargetAmount { get; set; }
    public DateTime TargetDate { get; set; }
}

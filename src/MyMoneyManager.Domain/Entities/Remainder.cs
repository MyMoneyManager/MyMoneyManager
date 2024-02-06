using MyMoneyManager.Domain.Commons;

namespace MyMoneyManager.Domain.Entities;

public class Remainder : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long GoalId { get; set; }
    public Goal Goal { get; set; }
    public string Description { get; set; }
    public DateTime TargetDate { get; set; }
}

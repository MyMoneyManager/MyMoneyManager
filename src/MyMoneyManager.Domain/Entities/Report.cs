using MyMoneyManager.Domain.Commons;
using MyMoneyManager.Domain.Enums;

namespace MyMoneyManager.Domain.Entities;

public class Report : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public TransactionType TransactionType { get; set; }
}

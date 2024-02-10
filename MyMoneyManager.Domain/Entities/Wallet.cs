using MyMoneyManager.Domain.Commons;

namespace MyMoneyManager.Domain.Entities;

public class Wallet : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public decimal Balance { get; set; }
    public string Description { get; set; }

    public IEnumerable<Transaction> Transactions { get; set; }
}

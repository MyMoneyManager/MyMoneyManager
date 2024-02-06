using MyMoneyManager.Domain.Commons;
using MyMoneyManager.Domain.Enums;

namespace MyMoneyManager.Domain.Entities;

public class Transaction : Auditable
{
    public long WalletId { get; set; }
    public Wallet Wallet { get; set; }
    public long CategoryId { get; set; }
    public Category Category { get; set; }
    public decimal Balance { get; set; }
    public string Description { get; set; }
    public TransactionType TransactionType { get; set; }
}

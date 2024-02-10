using MyMoneyManager.Domain.Enums;

namespace MyMoneyManager.Service.DTOs.Transactions;

public class TranzactionForCreationDto
{
    public long WalletId { get; set; }
    public long CategoryId { get; set; }
    public decimal Balance { get; set; }
    public string Description { get; set; }
    public TransactionType TransactionType { get; set; }
}

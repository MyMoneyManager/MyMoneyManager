using MyMoneyManager.Domain.Entities;

namespace MyMoneyManager.Service.DTOs.Wallets;

public class WalletCreationDto
{
    public long UserId { get; set; }
    public decimal Balance { get; set; }
    public string Description { get; set; }
}

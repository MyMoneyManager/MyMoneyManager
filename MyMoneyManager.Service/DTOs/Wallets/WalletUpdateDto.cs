namespace MyMoneyManager.Service.DTOs.Wallets
{
    public class WalletUpdateDto
    {
        public long UserId { get; set; }
        public decimal Balance { get; set; }
        public string Description { get; set; }
    }
}

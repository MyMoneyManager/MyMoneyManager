using MyMoneyManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyManager.Service.DTOs.Wallets
{
    public class WalletResultDto
    {
        public User User { get; set; }
        public decimal Balance { get; set; }
        public string Description { get; set; }
    }
}

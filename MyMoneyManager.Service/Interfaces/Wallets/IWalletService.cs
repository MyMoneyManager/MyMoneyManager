using MyMoneyManager.Domain.Configurations;
using MyMoneyManager.Service.DTOs.Wallets;

namespace MyMoneyManager.Service.Interfaces.Wallets;

public interface IWalletService
{
    Task<bool> RemoveAsync(long id);
    Task<WalletResultDto> AddAsync(WalletCreationDto dto);
    Task<WalletResultDto> ModifyAsync(long id, WalletUpdateDto dto);
    Task<WalletResultDto> RetrieveByIdAsync(long id);
    Task<List<WalletResultDto>> RetrieveAllAsync(PaginationParams @params);
}

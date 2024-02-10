using MyMoneyManager.Domain.Configurations;
using MyMoneyManager.Service.DTOs.Transactions;

namespace MyMoneyManager.Service.Interfaces.ITransactionServices;

public interface ITranzactionService
{
    Task<bool> RemoveAsync(long id);
    Task<TranzactionForResultDto> RetrieveByIdAsync(long id);
    Task<TranzactionForResultDto> AddAsync(TranzactionForCreationDto dto);
    Task<TranzactionForResultDto> ModifyAsync(long id, TranzactionForUpdateDto dto);
    Task<IEnumerable<TranzactionForResultDto>> RetrieveAllAsync(PaginationParams @params);
}

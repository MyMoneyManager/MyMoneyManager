using MyMoneyManager.Domain.Configurations;
using MyMoneyManager.Service.DTOs.Goals;

namespace MyMoneyManager.Service.Interfaces.IGoalService;

public interface IGoalService
{
    Task<bool> RemoveAsync(long Id);
    Task<GoalForResultDto> RetrieveByIdAsync(long Id);
    Task<GoalForResultDto> AddAsync(GoalForCreationDto dto);
    Task<GoalForResultDto> ModifyAsync(long id,GoalForUpdateDto dto);
    Task<IEnumerable<GoalForResultDto>> RetrieveAllAsync(PaginationParams @params);
}

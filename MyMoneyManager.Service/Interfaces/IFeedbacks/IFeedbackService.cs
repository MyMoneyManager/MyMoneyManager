using MyMoneyManager.Domain.Configurations;
using MyMoneyManager.Service.DTOs.Feedbacks;

namespace MyMoneyManager.Service.Interfaces.IFeedbacks;

public interface IFeedbackService
{
    Task<bool> DeleteAysnc (long id);
    Task<FeedbackForResultDto> AddAsync(FeedbackForCreationDto dto);
    Task<IEnumerable<FeedbackForResultDto>> RetrieveAllAsync(PaginationParams @params);
}

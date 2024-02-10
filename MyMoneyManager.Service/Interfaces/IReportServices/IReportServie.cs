using MyMoneyManager.Domain.Configurations;
using MyMoneyManager.Service.DTOs.Reports;

namespace MyMoneyManager.Service.Interfaces.IReportService;

public interface IReportServie
{
    Task<bool> RemoveAsync (long id);
    Task<ReportForResultDto> RetrieveByIdAsync (long id);
    Task<ReportForResultDto> AddAsync(ReportForCreationDto dto);
    Task<ReportForResultDto> ModifyAsync (long id,ReportForUpdateDto dto);
    Task<IEnumerable<ReportForResultDto>> RetrieveAllAsync (PaginationParams @params);
}

using MyMoneyManager.Domain.Configurations;
using MyMoneyManager.Service.DTOs.Permissions;

namespace MyMoneyManager.Service.Interfaces.IAuthorizations;

public interface IPermissionService
{
    Task<PermissionForResultDto> CreateAsync(PermissionForCreationDto dto);
    Task<bool> RemoveAsync(long id);
    Task<PermissionForResultDto> ModifyAsync(PermissionForUpdateDto dto);
    Task<PermissionForResultDto> RetrieveByIdAsync(long id);
    Task<List<PermissionForResultDto>> RetrieveAllAsync(PaginationParams @params);
}

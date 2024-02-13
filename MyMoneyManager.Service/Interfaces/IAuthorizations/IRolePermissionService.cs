using MyMoneyManager.Domain.Configurations;
using MyMoneyManager.Service.DTOs.RolePermisisons;

namespace MyMoneyManager.Service.Interfaces.IAuthorizations;

public interface IRolePermissionService
{
    Task<RolePermissionForResultDto> CreateAsync(RolePermissionForCreateDto permission);
    Task<RolePermissionForResultDto> ModifyAsync(RolePermissionForUpdateDto permission);
    Task<bool> RemoveAsync(long id);
    Task<RolePermissionForResultDto> RetrieveByIdAsync(long id);
    Task<List<RolePermissionForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<bool> CheckPermission(string role, string accessedMethod);
}

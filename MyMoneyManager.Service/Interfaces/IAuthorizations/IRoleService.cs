using MyMoneyManager.Domain.Configurations;
using MyMoneyManager.Domain.Entities.Authorizations;
using MyMoneyManager.Service.DTOs.Roles;

namespace MyMoneyManager.Service.Interfaces.IAuthorizations;

public interface IRoleService
{
    Task<RoleForResultDto> AddAsync(RoleForCreationDto dto);
    Task<bool> ModifyAsync(RoleForUpdateDto dto);
    Task<bool> RemoveAsync(long id);
    Task<IEnumerable<RoleForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<Role> RetrieveByIdForAuthAsync(long id);
    Task<RoleForResultDto> RetrieveByIdAsync(long id);
}

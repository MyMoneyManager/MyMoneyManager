using MyMoneyManager.Service.DTOs.Permissions;
using MyMoneyManager.Service.DTOs.Roles;

namespace MyMoneyManager.Service.DTOs.RolePermisisons;

public class RolePermissionForResultDto
{
    public long Id { get; set; }
    public long RoleId { get; set; }
    public RoleForResultDto Role { get; set; }

    public long PermissonId { get; set; }
    public PermissionForResultDto Permisson { get; set; }
}
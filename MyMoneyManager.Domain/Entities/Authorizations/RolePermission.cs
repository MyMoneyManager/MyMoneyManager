using MyMoneyManager.Domain.Commons;

namespace MyMoneyManager.Domain.Entities.Authorizations;

public class RolePermission : Auditable
{
    public long RoleId { get; set; }
    public Role Role { get; set; }

    public long PermissonId { get; set; }
    public Permission Permisson { get; set; }
}

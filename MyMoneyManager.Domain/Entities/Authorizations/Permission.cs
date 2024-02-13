using MyMoneyManager.Domain.Commons;

namespace MyMoneyManager.Domain.Entities.Authorizations;

public class Permission : Auditable
{
    public string Name { get; set; }
}

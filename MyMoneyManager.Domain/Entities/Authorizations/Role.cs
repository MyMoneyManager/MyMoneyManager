using MyMoneyManager.Domain.Commons;

namespace MyMoneyManager.Domain.Entities.Authorizations;

public class Role : Auditable
{
    public string Name { get; set; }
}

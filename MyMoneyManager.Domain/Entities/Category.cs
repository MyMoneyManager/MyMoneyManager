using MyMoneyManager.Domain.Commons;
using MyMoneyManager.Domain.Enums;

namespace MyMoneyManager.Domain.Entities;

public class Category : Auditable
{
    public CategoryType Name { get; set; }
}

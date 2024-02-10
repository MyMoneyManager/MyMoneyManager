using MyMoneyManager.Domain.Enums;

namespace MyMoneyManager.Service.DTOs.Categories;

public class CategoryForResultDto
{
    public long Id { get; set; }
    public CategoryType Name { get; set; }
}
